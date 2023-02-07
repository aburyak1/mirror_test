using System;
using System.Threading.Tasks;
using iikoTransport.Logging;
using iikoTransport.Logging.Metrics;
using iikoTransport.Postgres.Synchronization;
using iikoTransport.SbpService.Converters;
using iikoTransport.SbpService.IikoWebIntegration.Contracts;
using iikoTransport.SbpService.Infrastructure.Settings;
using iikoTransport.SbpService.Models;
using iikoTransport.SbpService.Storage.Contracts;
using iikoTransport.Service;
using iikoTransport.ServiceClient;

namespace iikoTransport.SbpService.IikoWebIntegration
{
    /// <summary>
    /// IikoWeb synchronization manager.
    /// </summary>
    public class IikoWebSyncManager : IIikoWebSyncManager
    {
        private readonly IServicesSettings servicesSettings;
        private readonly IIikoWebClient iikoWebClient;
        private readonly ISbpSettingsStorage sbpStorage;
        private readonly ISyncInfoStorage syncInfoStorage;
        private readonly ILog log;
        private readonly IMethodCallSettingsFactory callSettingsFactory;
        private readonly IMetrics metrics;

        public IikoWebSyncManager(
            IServicesSettings servicesSettings, 
            IIikoWebClient iikoWebClient, 
            ISbpSettingsStorage sbpStorage, 
            ISyncInfoStorage syncInfoStorage, 
            ILog log,
            IMethodCallSettingsFactory callSettingsFactory,
            IMetrics metrics)
        {
            this.servicesSettings = servicesSettings ?? throw new ArgumentNullException(nameof(servicesSettings));
            this.iikoWebClient = iikoWebClient ?? throw new ArgumentNullException(nameof(iikoWebClient));
            this.sbpStorage = sbpStorage ?? throw new ArgumentNullException(nameof(sbpStorage));
            this.syncInfoStorage = syncInfoStorage ?? throw new ArgumentNullException(nameof(syncInfoStorage));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.callSettingsFactory = callSettingsFactory ?? throw new ArgumentNullException(nameof(callSettingsFactory));
            this.metrics = metrics ?? throw new ArgumentNullException(nameof(metrics));
        }

        /// <inheritdoc />
        public async Task SyncSbpSettings(CallContext callContext)
        {
            if (callContext == null) throw new ArgumentNullException(nameof(callContext));
            
            log.Info("SyncSbpSettings started.");
            await RunSync(callContext);
            log.Info("SyncSbpSettings completed successfully.");
        }

        private async Task RunSync(CallContext callContext)
        {
            SyncInfo? lastSyncInfo = null;

            try
            {
                log.Debug("Getting last sync info from db...");
                lastSyncInfo = await GetLastSyncInfo();
                log.Debug($"Last sync info is: {lastSyncInfo}");

                log.Debug($"Getting SBP changes from iikoWeb by revision {lastSyncInfo.Revision}...");
                var (isEmpty, sbpChanges) = await GetSbpChanges(callContext, lastSyncInfo);
                if (isEmpty || sbpChanges?.NspkSettings == null)
                {
                    await LogSuccessSync(lastSyncInfo, lastSyncInfo.Revision);
                    return;
                }

                log.Debug("Start saving.");
                await sbpStorage.Upsert(sbpChanges.NspkSettings.Convert());
                log.Debug("Saving completed.");

                await LogSuccessSync(lastSyncInfo, sbpChanges.Revision);
            }
            catch (Exception e)
            {
                log.Error($"SyncSbpSettings completed with error: {e.Message}", e);
                await LogErrorSync(callContext, lastSyncInfo);
                throw;
            }
        }

        private async Task LogSuccessSync(SyncInfo lastSyncInfo, long currentTimestamp)
        {
            log.Info("Saving sync info to db...");
            await syncInfoStorage.RegisterSuccessSync(lastSyncInfo.Id, currentTimestamp, DateTime.UtcNow);
            log.Info("Saving sync info to db completed.");
        }

        private async Task<(bool IsEmpty, SbpSettingsChanges? SbpChanges)> GetSbpChanges(CallContext callContext, SyncInfo lastSyncInfo)
        {
            var iikoWebCallSettings = callSettingsFactory.CreateWithTimeout(callContext.CorrelationId, servicesSettings.IikoWebCallTimeout);
            var iikoWebRequest = new GetSbpSettingsChangesRequest(lastSyncInfo.Revision);

            try
            {
                var sbpChanges = await iikoWebClient.GetSbpSettingsChanges(iikoWebRequest, iikoWebCallSettings);
                var newSettingsCount = sbpChanges.NspkSettings?.Length ?? 0;
                metrics.Histogram("sync_sbp_settings", newSettingsCount);

                if (newSettingsCount == 0)
                {
                    log.Debug("Got empty SBP changes.");
                    return (true, null);
                }

                log.Info($"Got SBP changes. New revision: {sbpChanges.Revision}. Count: {newSettingsCount}.");
                return (false, sbpChanges);
            }
            catch (Exception)
            {
                metrics.Alert("sync_sbp_settings");
                throw;
            }
        }

        private async Task LogErrorSync(CallContext callContext, SyncInfo? lastSyncInfo)
        {
            if (lastSyncInfo != null)
            {
                log.Info("Saving error sync info to db...");
                string dbErrorMessage = $"See logs by correlationId: {callContext.CorrelationId}";
                await syncInfoStorage.RegisterErrorSync(lastSyncInfo.Id, dbErrorMessage, DateTime.UtcNow);
                log.Info("Saving error sync info to db completed.");
            }
            else
                log.Info("Unable to save error sync info to db because lastSyncInfo is null.");
        }

        private async Task<SyncInfo> GetLastSyncInfo()
        {
            return await syncInfoStorage.GetOrCreate(SyncSystemType.IikoWeb, SyncEntityType.Sbp, 0);
        }
    }
}