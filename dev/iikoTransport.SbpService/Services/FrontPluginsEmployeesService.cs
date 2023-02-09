using System;
using System.Threading.Tasks;
using iikoTransport.Common.Contracts;
using iikoTransport.SbpService.Contracts.FrontPlugin;
using iikoTransport.SbpService.Converters;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.SbpService.Services.SbpNspk;
using iikoTransport.SbpService.Storage.Contracts;
using iikoTransport.SbpService.Storage.Contracts.Entities;
using iikoTransport.Service;

namespace iikoTransport.SbpService.Services
{
    /// <summary>
    /// Service for front plugins.
    /// </summary>
    public class FrontPluginsSbpService : IFrontPluginsSbpService
    {
        private readonly ISbpSettingsStorage settingsStorage;
        private readonly SbpNspkClient sbpClient;

        public FrontPluginsSbpService(ISbpSettingsStorage settingsStorage, SbpNspkClient sbpClient)
        {
            this.settingsStorage = settingsStorage ?? throw new ArgumentNullException(nameof(settingsStorage));
            this.sbpClient = sbpClient ?? throw new ArgumentNullException(nameof(sbpClient));
        }
        
        /// <inheritdoc />
        public async Task<PaymentLinkPayloadResponse> CreateOneTimePaymentLink(Call<CreateOneTimePaymentLinkRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var setting = await GetSbpSetting(call.Context);
            var request = call.Payload.Convert(setting, sbpClient.AgentId);
            var response = await sbpClient.CreateAndGetOneTimePaymentLinkPayloadForB2B(call.Context.CorrelationId, request);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<PaymentLinkPayloadResponse> CreateReusablePaymentLink(Call<CreateReusablePaymentLinkRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var setting = await GetSbpSetting(call.Context);
            var request = call.Payload.Convert(setting, sbpClient.AgentId);
            var response = await sbpClient.CreateAndGetReusablePaymentLinkPayloadForB2B(call.Context.CorrelationId, request);
            return response.Convert();
        }
        
        /// <inheritdoc />
        public async Task<PaymentLinkPayloadResponse> GetQrcPayload(Call<GetQrcPayloadRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));
            
            var response = await sbpClient.GetQRCPayload(call.Context.CorrelationId, call.Payload.QrcId);
            return response.Convert();
        }
        
        /// <inheritdoc />
        public async Task<CreateQrcIdReservationResponse> CreateQrcIdReservation(Call<CreateQrcIdReservationRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var response = await sbpClient.CreateQrcIdReservationV1(call.Context.CorrelationId,
                new SbpNspk.Contracts.CreateQrcIdReservationV1Request(call.Payload.Quantity));
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<PaymentLinkPayloadResponse> СreateCashRegisterQr(Call<CreateCashRegisterQrRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var setting = await GetSbpSetting(call.Context);
            var request = call.Payload.Convert(setting, sbpClient.AgentId);
            var response = await sbpClient.СreateCashRegisterQr(call.Context.CorrelationId, request);
            return response.Convert();
        }
        
        /// <inheritdoc />
        public async Task<ActivateCashRegisterQrResponse> ActivateCashRegisterQr(Call<ActivateCashRegisterQrRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var response = await sbpClient.CreateParams(call.Context.CorrelationId, call.Payload.QrcId, 
                new SbpNspk.Contracts.CreateParamsRequest(call.Payload.Amount));
            return response.Convert();
        }
        
        /// <inheritdoc />
        public async Task<DeactivateCashRegisterQrResponse> DeactivateCashRegisterQr(Call<DeactivateCashRegisterQrRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));
            
            var response = await sbpClient.DeleteParams(call.Context.CorrelationId, call.Payload.QrcId);
            return new DeactivateCashRegisterQrResponse(response.Code, response.Message, response.Data);
        }
        
        /// <inheritdoc />
        public async Task<GetStatusQrcOperationsResponse> GetStatusQrcOperations(Call<GetStatusQRCOperationsRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var response = await sbpClient.GetStatusQRCOperations(call.Context.CorrelationId,
                new SbpNspk.Contracts.GetStatusQRCOperationsRequest(call.Payload.QrcIds));
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<CreatePaymentPetitionResponse> CreatePaymentPetition(Call<CreatePaymentPetitionRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var setting = await GetSbpSetting(call.Context);
            var request = call.Payload.Convert(setting);
            var response = await sbpClient.CreatePaymentPetition(call.Context.CorrelationId, call.Payload.TrxId, request);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<GetRefundStatusResponse> GetRefundStatus(Call<GetRefundStatusRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var response = await sbpClient.RefundRequestStatusV2(call.Context.CorrelationId, call.Payload.OriginalTrxId,
                call.Payload.OpkcRefundRequestId);
            return response.Convert();
        }

        private async Task<SbpSetting> GetSbpSetting(ICallContext callContext)
        {
            // Get appId header - in this case it's terminal group id.
            string? tgIdHeader = callContext.Headers.GetAppId();
            if (!Guid.TryParse(tgIdHeader, out var tgId))
                throw new InvalidOperationException($"Unable to parse appId header as guid: {tgIdHeader}");

            var setting = await settingsStorage.Get(tgId);
            return setting;
        }
    }
}