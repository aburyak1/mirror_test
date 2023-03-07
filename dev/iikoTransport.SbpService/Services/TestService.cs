﻿using System;
using System.Threading.Tasks;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.SbpService.Services.SbpNspk;
using iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations;
using iikoTransport.SbpService.Storage.Contracts;
using iikoTransport.Utils;

namespace iikoTransport.SbpService.Services
{
    /// <summary>
    /// Сервис для работы с api СБП.
    /// </summary>
    public class TestService : ITestService
    {
        private readonly ISbpSettingsStorage settingsStorage;
        private readonly SbpNspkClient nspkClient;

        public TestService(ISbpSettingsStorage sbpStorage, SbpNspkClient nspkClient)
        {
            this.settingsStorage = sbpStorage ?? throw new ArgumentNullException(nameof(sbpStorage));
            this.nspkClient = nspkClient ?? throw new ArgumentNullException(nameof(nspkClient));
        }
        
        public async Task<string> TestCreateQrcIdReservation()
        {
            var result = await nspkClient.CreateQrcIdReservationV1(Guid.NewGuid(), 10);
            return result.ToJson();
        }

        public async Task<string> TestCreateAndGetOneTimePaymentLinkPayloadForB2B(Guid? tgId = null)
        {
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
            
            var result = await nspkClient.CreateAndGetOneTimePaymentLinkPayloadForB2B(Guid.NewGuid(), request);
            return result.ToJson();
        }
        
        public async Task<string> TestGetQRCPayload(string? qrcId = null)
        {
            var result = await nspkClient.GetQRCPayload(Guid.NewGuid(), qrcId ?? "AO1000670LSS7DN18SJQDNP4B05KLJL2");
            return result.ToJson();
        }
    }
}