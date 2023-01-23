using System;
using System.Net.Http;
using System.Threading.Tasks;
using iikoTransport.Logging;
using iikoTransport.ServiceClient;

namespace iikoTransport.SbpService.Client.Scheduler
{
    public class SbpServiceClient : BaseServiceClient, ISbpServiceClient
    {
        public SbpServiceClient(
            HttpClient httpClient,
            SbpServiceClientOptions options,
            IMethodCallSettingsFactory callSettingsFactory,
            ILog log)
            : base(httpClient, options, callSettingsFactory, log)
        {
        }

        protected override string ControllerName
        {
            get => "SchedulerSbp";
            set => throw new InvalidOperationException();
        }

        /// <inheritdoc />
        public async Task SyncSettingsWithIikoWeb(MethodCallSettings? callSettings)
        {
            await ExecuteVoidAsync(nameof(SyncSettingsWithIikoWeb), new object(), callSettings);
        }
    }
}