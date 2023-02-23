using System;
using System.Threading.Tasks;
using iikoTransport.Logging;
using iikoTransport.SbpService.Converters;
using iikoTransport.SbpService.FrontClient;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.SbpService.Services.SbpNspk.Contracts.Events;
using iikoTransport.SbpService.Storage.Contracts;
using iikoTransport.Service;
using iikoTransport.ServiceClient;
using RouteNames = iikoTransport.SbpService.Contracts.FrontPlugin.ToFront.RouteNames;

namespace iikoTransport.SbpService.Services
{
    /// <summary>
    /// Сервис для работы с api СБП.
    /// </summary>
    public class SbpEventsService : ISbpEventsService
    {
        private readonly IPaymentLinksStorage paymentLinksStorage;
        private readonly IRefundRequestsStorage refundRequestsStorage;
        private readonly ISbpFrontClient frontClient;
        private readonly IMethodCallSettingsFactory callSettingsFactory;
        private readonly ILog log;
        // todo: По готовности плагина СБП узнать его licenseModuleId и вписать сюда
        private const int SbpPluginModuleId = 0;

        public SbpEventsService(IPaymentLinksStorage paymentLinksStorage, IRefundRequestsStorage refundRequestsStorage, ISbpFrontClient frontClient,
            IMethodCallSettingsFactory callSettingsFactory, ILog log)
        {
            this.paymentLinksStorage = paymentLinksStorage ?? throw new ArgumentNullException(nameof(paymentLinksStorage));
            this.refundRequestsStorage = refundRequestsStorage ?? throw new ArgumentNullException(nameof(refundRequestsStorage));
            this.frontClient = frontClient ?? throw new ArgumentNullException(nameof(frontClient));
            this.callSettingsFactory = callSettingsFactory ?? throw new ArgumentNullException(nameof(callSettingsFactory));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public async Task SendFinalStatusAckV2(Call<SendFinalStatusAckV2Request> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));
            var request = call.Payload;

            log.Debug($"SendFinalStatusAckV2 {request.QrcId}|{request.Status}|{request.Amount}|{request.Timestamp}.");
            var paymentLink = await paymentLinksStorage.Get(request.QrcId);
            if (paymentLink == null)
                throw new Exception($"Creator of payment linq with qrcId {request.QrcId} not found.");
            var requestToFront = request.Convert();
            var callSettings = callSettingsFactory.CreateFromContext(call.Context);
            await frontClient.CallFrontPluginMethod(RouteNames.SendFinalStatusAck, requestToFront, paymentLink.TerminalGroupUocId, SbpPluginModuleId,
                callSettings);
        }

        public async Task SendFinalStatusB2CAck(Call<SendFinalStatusB2CAckRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));
            var request = call.Payload;

            log.Debug($"SendFinalStatusB2CAck {request.OriginalQrcId}|{request.Status}|{request.Amount}|{request.Timestamp}.");
            var originalRefundRequest = await refundRequestsStorage.Get(request.OpkcRefundRequestId, request.TrxId);
            if (originalRefundRequest == null)
                throw new Exception($"Creator of refund request with opkcRefundRequestId {request.OpkcRefundRequestId} not found.");
            var requestToFront = request.Convert();
            var callSettings = callSettingsFactory.CreateFromContext(call.Context);
            await frontClient.CallFrontPluginMethod(RouteNames.SendFinalStatusB2CAck, requestToFront, originalRefundRequest.TerminalGroupUocId,
                SbpPluginModuleId, callSettings);
        }

        public async Task RefundResolution(Call<RefundResolutionRequest> call)
        {
            if (call == null) throw new ArgumentNullException(nameof(call));
            var request = call.Payload;

            log.Debug(
                $"RefundResolution {request.Code}|{request.Message}|{request.Status}|{request.OpkcRefundRequestId}|{request.OriginalTrxId}|{request.AgentRefundRequestId}.");
            var originalRefundRequest = await refundRequestsStorage.Get(request.OpkcRefundRequestId, request.OriginalTrxId);
            if (originalRefundRequest == null)
                throw new Exception($"Creator of refund request with opkcRefundRequestId {request.OpkcRefundRequestId} not found.");
            var requestToFront = request.Convert();
            var callSettings = callSettingsFactory.CreateFromContext(call.Context);
            await frontClient.CallFrontPluginMethod(RouteNames.RefundResolution, requestToFront, originalRefundRequest.TerminalGroupUocId,
                SbpPluginModuleId, callSettings);
        }
    }
}