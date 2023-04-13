using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using iikoTransport.Common.Contracts;
using iikoTransport.Logging;
using iikoTransport.ServiceClient;
using iikoTransport.TransportService.Client.Transport;
using iikoTransport.TransportService.Contracts.Transport;

namespace iikoTransport.SbpService.FrontClient
{
    public class SbpFrontClient : BaseProxyTransportServiceClient, ISbpFrontClient
    {
        private readonly int expirationMs;

        public SbpFrontClient(
            HttpClient httpClient,
            SbpFrontClientOptions options,
            IMethodCallSettingsFactory callSettingsFactory,
            ILog log)
            : base(httpClient, options, callSettingsFactory, log)
        {
            expirationMs = (int) options.Expiration.TotalMilliseconds;
        }

        public async Task CallFrontPluginMethod(string methodUri, object bodyJson, Guid terminalGroupUocId, Guid? terminalId, int pluginModuleId,
            MethodCallSettings callSettings)
        {
            if (string.IsNullOrWhiteSpace(methodUri)) throw new ArgumentNullException(methodUri);
            if (callSettings == null) throw new ArgumentNullException(nameof(callSettings));

            var headers = new Dictionary<string, string>
            {
                { TransportConstants.Headers.LicenseModuleId, pluginModuleId.ToString() },
                { TransportConstants.Headers.IsCloudCall, true.ToString() }
            };
            if (terminalId.HasValue)
            {
                headers.Add(TransportConstants.Headers.TerminalId, terminalId.Value.ToString());
            }

            var pluginRequest = new RequestToExternalSystem(ExternalSystemType.Front, terminalGroupUocId.ToString(), methodUri, expirationMs,
                bodyJson, headers);
            await RunOnExternalSystem(pluginRequest, callSettings);
        }
    }
}