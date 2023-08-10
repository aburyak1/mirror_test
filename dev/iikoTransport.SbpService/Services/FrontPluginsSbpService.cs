using System;
using System.Threading.Tasks;
using iikoTransport.Common.Contracts;
using iikoTransport.Logging;
using iikoTransport.SbpService.Contracts.FrontPlugin.FromFront;
using iikoTransport.SbpService.Converters.FrontPlugin;
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

            var request = call.Payload;
            var tgId = GetTerminalGroupUocId(call.Context);
            var setting = await settingsStorage.Get(tgId, call.Context.CancellationToken);
            var requestToSbp = request.Convert(setting, sbpClient.AgentId);
            var response = await sbpClient.CreateQRC(call.Context.CorrelationId, requestToSbp, request.MediaType,
                request.Width, request.Height);

            var terminalId = GetTerminalId(call.Context);
            await SavePaymentLinkInfo(response, tgId, terminalId, requestToSbp.MerchantId);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<PaymentLinkPayloadResponse> CreateReusablePaymentLink(Call<CreateReusablePaymentLinkRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var request = call.Payload;
            var tgId = GetTerminalGroupUocId(call.Context);
            var setting = await settingsStorage.Get(tgId, call.Context.CancellationToken);
            var requestToSbp = request.Convert(setting, sbpClient.AgentId);
            var response = await sbpClient.CreateQRC(call.Context.CorrelationId, requestToSbp, request.MediaType,
                request.Width, request.Height);

            var terminalId = GetTerminalId(call.Context);
            await SavePaymentLinkInfo(response, tgId, terminalId, requestToSbp.MerchantId);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<PaymentLinkPayloadResponse> GetQrcPayload(Call<GetQrcPayloadRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var request = call.Payload;
            var response = await sbpClient.GetQRCPayload(call.Context.CorrelationId, request.QrcId, request.MediaType, request.Width, request.Height);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<GetStatusQrcOperationsResponse> GetStatusQrcOperations(Call<GetStatusQrcOperationsRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var response = await sbpClient.GetStatusQRCOperations(call.Context.CorrelationId,
                new Nspk.GetStatusQRCOperationsRequest(call.Payload.QrcIds));
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

            var request = call.Payload;
            var tgId = GetTerminalGroupUocId(call.Context);
            var setting = await settingsStorage.Get(tgId, call.Context.CancellationToken);
            var requestToSbp = call.Payload.Convert(setting, sbpClient.AgentId);
            var response = await sbpClient.СreateCashRegisterQr(call.Context.CorrelationId, requestToSbp, request.MediaType, request.Width,
                request.Height);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<ActivateCashRegisterQrResponse> ActivateCashRegisterQr(Call<ActivateCashRegisterQrRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var tgId = GetTerminalGroupUocId(call.Context);
            var response = await sbpClient.CreateParams(call.Context.CorrelationId, call.Payload.QrcId,
                new Nspk.CreateParamsRequest(call.Payload.Amount));

            var terminalId = GetTerminalId(call.Context);
            await SaveCashRegPaymentLinkInfo(response, tgId, terminalId, call.Payload.QrcId);
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
        public async Task<GetCashRegQrStatusResponse> GetCashRegQrStatus(Call<GetCashRegQrStatusRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var response = await sbpClient.GetCashRegQrStatus(call.Context.CorrelationId, call.Payload.QrcId);
            return response.Convert();
        }

        /// <inheritdoc />
        public async Task<GetStatusCashRegQrOperationResponse> GetStatusCashRegQrOperation(Call<GetStatusCashRegQrOperationRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));

            var response = await sbpClient.StatusCashRegQr(call.Context.CorrelationId, call.Payload.QrcId, call.Payload.ParamsId);
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

            var terminalId = GetTerminalId(call.Context);
            await SaveRefundRequestInfo(response, call.Payload.TrxId, tgId, terminalId, requestToSbp.MerchantId);
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
            var tgIdHeader = callContext.Headers.GetAppId();
            if (!Guid.TryParse(tgIdHeader, out var tgId))
                throw new InvalidOperationException($"Unable to parse appId header as guid: {tgIdHeader}");
            return tgId;
        }

        private Guid? GetTerminalId(ICallContext callContext)
        {
            var trIdHeader = callContext.Headers.GetTerminalId();
            if (!Guid.TryParse(trIdHeader, out var trId))
                return null;
            return trId;
        }

        private async Task SavePaymentLinkInfo(Nspk.SbpNspkResponse<Nspk.QrcPayloadResponse> sbpResponse, Guid terminalGroupUocId, Guid? terminalId, string merchantId)
        {
            if (!sbpResponse.Code.Equals(Nspk.PaymentApiResponseCode.RQ00000.ToString()) || sbpResponse.Data == null)
            {
                log.Error($"Failed payment link creation for merchant={merchantId} from uocTerminalGroup={terminalGroupUocId}. " +
                          $"Error {sbpResponse.Code} {sbpResponse.Message}");
                return;
            }

            if (string.IsNullOrWhiteSpace(sbpResponse.Data.QrcId))
            {
                log.Error($"Failed payment link creation for merchant={merchantId} from uocTerminalGroup={terminalGroupUocId}. Empty data. ");
                return;
            }

            var paymentLink = new PaymentLink(Guid.NewGuid(), sbpResponse.Data.QrcId, null, terminalGroupUocId, terminalId, DateTime.UtcNow);
            await paymentLinksStorage.Upsert(paymentLink);
            log.Info($"Payment link saved with id={paymentLink.QrcId} for merchant={merchantId} from uocTerminalGroup={terminalGroupUocId}. ");
        }

        private async Task SaveCashRegPaymentLinkInfo(Nspk.SbpNspkResponse<Nspk.CreateParamsResponse> sbpResponse, Guid terminalGroupUocId,
            Guid? terminalId, string requestQrId)
        {
            if (!sbpResponse.Code.Equals(Nspk.PaymentApiResponseCode.RQ00000.ToString()) || sbpResponse.Data == null)
            {
                log.Error($"Failed payment link activation for qrId={requestQrId} from uocTerminalGroup={terminalGroupUocId}. " +
                          $"Error {sbpResponse.Code} {sbpResponse.Message}");
                return;
            }

            if (string.IsNullOrWhiteSpace(sbpResponse.Data.QrcId) || string.IsNullOrWhiteSpace(sbpResponse.Data.ParamsId))
            {
                log.Error($"Failed payment link activation for qrId={requestQrId} from uocTerminalGroup={terminalGroupUocId}. Empty data. ");
                return;
            }

            var paymentLink = new PaymentLink(Guid.NewGuid(), sbpResponse.Data.QrcId, sbpResponse.Data.ParamsId, terminalGroupUocId, terminalId,
                DateTime.UtcNow);
            await paymentLinksStorage.Upsert(paymentLink);
            log.Info($"Payment link params saved with {paymentLink.QrcId}|{paymentLink.ParamsId} from uocTerminalGroup={terminalGroupUocId}. ");
        }

        private async Task SaveRefundRequestInfo(Nspk.SbpNspkResponse<Nspk.CreatedRefundResponse> sbpResponse, string trxId, Guid terminalGroupUocId,
            Guid? terminalId, string merchantId)
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

            var refundRequest = new RefundRequest(Guid.NewGuid(), sbpResponse.Data.OpkcRefundRequestId, trxId, terminalGroupUocId, terminalId,
                DateTime.UtcNow);
            await refundRequestsStorage.Upsert(refundRequest);
            log.Info($"Refund request saved with id={refundRequest.OpkcRefundRequestId} " +
                     $"for merchant={merchantId} from uocTerminalGroup={terminalGroupUocId}. ");
        }
    }
}