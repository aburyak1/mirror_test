using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using iikoTransport.Logging;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.SbpService.Services.SbpNspk;
using iikoTransport.SbpService.Services.SbpNspk.Contracts;
using iikoTransport.SbpService.Storage.Contracts;
using iikoTransport.Utils;
using Microsoft.Extensions.Caching.Distributed;

namespace iikoTransport.SbpService.Services
{
    /// <summary>
    /// Сервис для работы с api СБП.
    /// </summary>
    public class SbpService : ISbpService
    {
        private readonly ISbpSettingsStorage settingsStorage;
        private readonly SbpNspkClient nspkClient;
        private readonly ILog log;
        private readonly IDistributedCache testCache;
        private const string testCacheKey = "testKey0";

        public SbpService(ISbpSettingsStorage sbpStorage, SbpNspkClient nspkClient, ILog log, IDistributedCache distributedCache)
        {
            this.settingsStorage = sbpStorage ?? throw new ArgumentNullException(nameof(sbpStorage));
            this.nspkClient = nspkClient ?? throw new ArgumentNullException(nameof(nspkClient));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.testCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }
        
        public async Task SetPassword(string password)
        {
            var passwordBytes = Encoding.Default.GetBytes(password);
            await testCache.SetAsync(testCacheKey, passwordBytes, new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(30) });
        }
        
        public async Task<string> TestRun1()
        {
            var cert = await GetCert();
            var request = new CreateQrcIdReservationV1Request(10);
            var result = await nspkClient.CreateQrcIdReservationV1(Guid.NewGuid(), request, cert);
            return result.ToJson();
        }

        public async Task<string> TestRun2(Guid? tgId = null)
        {
            var cert = await GetCert();
            CreateAndGetOneTimePaymentLinkPayloadForB2BRequest request;
            if (tgId.HasValue)
            {
                var setting = await settingsStorage.Get(tgId.Value);
                request = new CreateAndGetOneTimePaymentLinkPayloadForB2BRequest(
                    nspkClient.AgentId,
                    setting.MemberId,
                    setting.Account,
                    setting.MerchantId,
                    "100", 
                    "Test", 
                    false);
            }
            else
            {
                request = new CreateAndGetOneTimePaymentLinkPayloadForB2BRequest(
                    nspkClient.AgentId,
                    string.Empty, 
                    string.Empty, 
                    string.Empty, 
                    "100", 
                    "Test", 
                    false);
            }
            
            var result = await nspkClient.CreateAndGetOneTimePaymentLinkPayloadForB2B(Guid.NewGuid(), request, cert);
            return result.ToJson();
        }
        
        public async Task<string> TestRun3(string? qrcId = null)
        {
            var cert = await GetCert();
            var result = await nspkClient.GetQRCPayload(Guid.NewGuid(), qrcId ?? "AO1000670LSS7DN18SJQDNP4B05KLJL2", cert);
            return result.ToJson();
        }

        private async Task<X509Certificate2> GetCert()
        {
            var passwordBytes = await testCache.GetAsync(testCacheKey);
            var password = Encoding.Default.GetString(passwordBytes ?? throw new NullReferenceException("No password in TestCache. "));
            var cert = new X509Certificate2("Services\\SbpNspk\\myreq_2022.pfx", password);
            return cert;
        }
    }
}