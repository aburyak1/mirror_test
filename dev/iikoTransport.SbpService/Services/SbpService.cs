using System;
using System.Threading.Tasks;
using iikoTransport.Logging;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.SbpService.Services.SbpNspk.Contracts;
using iikoTransport.SbpService.Storage.Contracts;

namespace iikoTransport.SbpService.Services
{
    /// <summary>
    /// Сервис для работы с api СБП.
    /// </summary>
    public class SbpService : ISbpService
    {
        private readonly ISbpSettingsStorage sbpStorage;
        private readonly ILog log;
        private readonly SbpNspkClient nspkClient;

        private readonly string agentId = string.Empty;

        public SbpService(ISbpSettingsStorage sbpStorage, SbpNspkClient nspkClient, ILog log)
        {
            this.sbpStorage = sbpStorage ?? throw new ArgumentNullException(nameof(sbpStorage));
            this.nspkClient = nspkClient ?? throw new ArgumentNullException(nameof(nspkClient));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }
        
        public async Task<string> TestRun()
        {
            var request = new CreateAndGetOneTimePaymentLinkPayloadForB2BRequest(
                agentId,
                string.Empty, 
                string.Empty, 
                string.Empty, 
                string.Empty, 
                string.Empty, 
                false);
            return await nspkClient.TestMethod(request);
        }
    }
}