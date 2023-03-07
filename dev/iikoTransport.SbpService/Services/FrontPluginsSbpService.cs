using System;
using System.Threading.Tasks;
using iikoTransport.Common.Contracts;
using iikoTransport.Logging;
using iikoTransport.SbpService.Contracts.FrontPlugin.FromFront;
using iikoTransport.SbpService.Converters;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.SbpService.Services.SbpNspk;
using iikoTransport.SbpService.Storage.Contracts;
using iikoTransport.SbpService.Storage.Contracts.Entities;
using iikoTransport.Service;
using Nspk = iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations;

namespace iikoTransport.SbpService.Services
{
    /// <summary>
    /// Service for front plugins.
    /// </summary>
    public class FrontPluginsSbpService : IFrontPluginsSbpService
    {
        private readonly ISbpSettingsStorage settingsStorage;
        private readonly IPaymentLinksStorage paymentLinksStorage;
        private readonly IRefundRequestsStorage refundRequestsStorage;
        private readonly SbpNspkClient sbpClient;
        private readonly ILog log;

        public FrontPluginsSbpService(ISbpSettingsStorage settingsStorage, IPaymentLinksStorage paymentLinksStorage,
            IRefundRequestsStorage refundRequestsStorage, SbpNspkClient sbpClient, ILog log)
        {
            this.settingsStorage = settingsStorage ?? throw new ArgumentNullException(nameof(settingsStorage));
            this.paymentLinksStorage = paymentLinksStorage ?? throw new ArgumentNullException(nameof(paymentLinksStorage));
            this.refundRequestsStorage = refundRequestsStorage ?? throw new ArgumentNullException(nameof(refundRequestsStorage));
            this.sbpClient = sbpClient ?? throw new ArgumentNullException(nameof(sbpClient));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <inheritdoc />
        public async Task<PaymentLinkPayloadResponse> CreateOneTimePaymentLink(Call<CreateOneTimePaymentLinkRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var tgId = GetTerminalGroupUocId(call.Context);
            var setting = await settingsStorage.Get(tgId, call.Context.CancellationToken);
            var requestToSbp = call.Payload.Convert(setting, sbpClient.AgentId);
            var response = await sbpClient.CreateAndGetOneTimePaymentLinkPayloadForB2B(call.Context.CorrelationId, requestToSbp);

            await SavePaymentLinkInfo(response, tgId, requestToSbp.MerchantId);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<PaymentLinkPayloadResponse> CreateReusablePaymentLink(Call<CreateReusablePaymentLinkRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var tgId = GetTerminalGroupUocId(call.Context);
            var setting = await settingsStorage.Get(tgId, call.Context.CancellationToken);
            var requestToSbp = call.Payload.Convert(setting, sbpClient.AgentId);
            var response = await sbpClient.CreateAndGetReusablePaymentLinkPayloadForB2B(call.Context.CorrelationId, requestToSbp);

            await SavePaymentLinkInfo(response, tgId, requestToSbp.MerchantId);
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

            var response = await sbpClient.CreateQrcIdReservationV1(call.Context.CorrelationId, call.Payload.Quantity);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<PaymentLinkPayloadResponse> СreateCashRegisterQr(Call<CreateCashRegisterQrRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var tgId = GetTerminalGroupUocId(call.Context);
            var setting = await settingsStorage.Get(tgId, call.Context.CancellationToken);
            var requestToSbp = call.Payload.Convert(setting, sbpClient.AgentId);
            var response = await sbpClient.СreateCashRegisterQr(call.Context.CorrelationId, requestToSbp);

            await SavePaymentLinkInfo(response, tgId, requestToSbp.MerchantId);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<ActivateCashRegisterQrResponse> ActivateCashRegisterQr(Call<ActivateCashRegisterQrRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var response = await sbpClient.CreateParams(call.Context.CorrelationId, call.Payload.QrcId,
                new Nspk.CreateParamsRequest(call.Payload.Amount));
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
                new Nspk.GetStatusQRCOperationsRequest(call.Payload.QrcIds));
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<CreatedRefundResponse> CreateRefundRequest(Call<CreateRefundRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var tgId = GetTerminalGroupUocId(call.Context);
            var setting = await settingsStorage.Get(tgId, call.Context.CancellationToken);
            var requestToSbp = call.Payload.Convert(setting);
            var response = await sbpClient.CreateRefundRequest(call.Context.CorrelationId, call.Payload.TrxId, requestToSbp);

            await SaveRefundRequestInfo(response, call.Payload.TrxId, tgId, requestToSbp.MerchantId);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<CreatedRefundResponse> GetRefundIdRequest(Call<GetRefundIdRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var response = await sbpClient.GetRefundIdRequest(call.Context.CorrelationId, call.Payload.TrxId, call.Payload.AgentRefundRequestId);
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

        private Guid GetTerminalGroupUocId(ICallContext callContext)
        {
            // Get appId header - in this case it's terminal group id.
            string? tgIdHeader = callContext.Headers.GetAppId();
            if (!Guid.TryParse(tgIdHeader, out var tgId))
                throw new InvalidOperationException($"Unable to parse appId header as guid: {tgIdHeader}");
            return tgId;
        }

        private async Task SavePaymentLinkInfo(Nspk.SbpNspkResponse<Nspk.QrcPayloadResponse> sbpResponse, Guid terminalGroupUocId, string merchantId)
        {
            if (!sbpResponse.Code.Equals(Nspk.PaymentApiResponseCode.RQ00000.ToString()) || sbpResponse.Data == null)
            {
                log.Error($"Failed payment link creation for merchant={merchantId} from uocTerminalGroup={terminalGroupUocId}. " +
                          "Error {sbpResponse.Code} {sbpResponse.Message}");
                return;
            }

            if (string.IsNullOrWhiteSpace(sbpResponse.Data.QrcId))
            {
                log.Error($"Failed payment link creation for merchant={merchantId} from uocTerminalGroup={terminalGroupUocId}. Empty data. ");
                return;
            }

            var paymentLink = new PaymentLink(sbpResponse.Data!.QrcId, terminalGroupUocId, DateTime.UtcNow);
            await paymentLinksStorage.Upsert(paymentLink);
            log.Info($"Payment link saved with id={paymentLink.QrcId} for merchant={merchantId} from uocTerminalGroup={terminalGroupUocId}. ");
        }

        private async Task SaveRefundRequestInfo(Nspk.SbpNspkResponse<Nspk.CreatedRefundResponse> sbpResponse, string trxId, Guid terminalGroupUocId,
            string merchantId)
        {
            if (!sbpResponse.Code.Equals(Nspk.PaymentApiResponseCode.RQ00000.ToString()) || sbpResponse.Data == null)
            {
                log.Error($"Failed refund request creation for merchant={merchantId} from uocTerminalGroup={terminalGroupUocId}. " +
                          "Error {sbpResponse.Code} {sbpResponse.Message}");
                return;
            }

            if (string.IsNullOrWhiteSpace(sbpResponse.Data.OpkcRefundRequestId))
            {
                log.Error($"Failed refund request creation for merchant={merchantId} from uocTerminalGroup={terminalGroupUocId}. Empty data. ");
                return;
            }

            var refundRequest = new RefundRequest(sbpResponse.Data!.OpkcRefundRequestId, trxId, terminalGroupUocId);
            await refundRequestsStorage.Upsert(refundRequest);
            log.Info(
                $"Refund request saved with id={refundRequest.OpkcRefundRequestId} for merchant={merchantId} from uocTerminalGroup={terminalGroupUocId}. ");
        }
    }
}