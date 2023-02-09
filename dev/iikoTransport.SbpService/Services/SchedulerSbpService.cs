using System;
using System.Threading.Tasks;
using iikoTransport.SbpService.IikoWebIntegration;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.Service;

namespace iikoTransport.SbpService.Services
{
    /// <summary>
    /// Сервис для обработки запросов от SchedulerService.
    /// </summary>
    public class SchedulerSbpService : ISchedulerSbpService
    {
        private readonly IIikoWebSyncManager iikoWebSyncManager;

        public SchedulerSbpService(IIikoWebSyncManager iikoWebSyncManager)
        {
            this.iikoWebSyncManager = iikoWebSyncManager;
        }

        /// <inheritdoc />
        public async Task SyncSbpSettingsWithIikoWeb(CallContext callContext)
        {
            if (callContext == null) throw new ArgumentNullException(nameof(callContext));
            await iikoWebSyncManager.SyncSbpSettings(callContext);
        }
    }
}