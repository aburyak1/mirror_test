using System.Linq;
using iikoTransport.SbpService.Storage.Contracts.Entities;
using Front = iikoTransport.SbpService.Contracts.FrontPlugin.FromFront;
using Sbp = iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations;

namespace iikoTransport.SbpService.Converters.FrontPlugin
{
    /// <summary>
    /// Converters from sbp.nspk contracts to front plugin contracts.
    /// </summary>
    public static class SbpOperationsToFrontPluginConverters
    {
        private const string QrStaticType = "01";
        private const string QrDynamicType = "02";
        
        public static Front.PaymentLinkPayloadResponse Convert(this Sbp.SbpNspkResponse<Sbp.QrcPayloadResponse> source)
        {
            return new Front.PaymentLinkPayloadResponse(source.Code, source.Message, source.Data?.Convert());
        }

        public static Sbp.CreateQRCRequest Convert(
            this Front.CreateOneTimePaymentLinkRequest source, SbpSetting settings, string agentId)
        {
            return new Sbp.CreateQRCRequest(
                agentId,
                settings.MemberId,
                settings.Account,
                settings.MerchantId,
                QrDynamicType,
                source.Amount,
                source.QrTtl,
                source.PaymentPurpose);
        }

        public static Sbp.CreateQRCRequest Convert(
            this Front.CreateReusablePaymentLinkRequest source, SbpSetting settings, string agentId)
        {
            return new Sbp.CreateQRCRequest(
                agentId,
                settings.MemberId,
                settings.Account,
                settings.MerchantId,
                QrStaticType,
                source.Amount,
                null,
                source.PaymentPurpose);
        }

        public static Front.GetStatusQrcOperationsResponse Convert(this Sbp.SbpNspkResponse<Sbp.GetStatusQRCOperationsResponse[]> source)
        {
            return new Front.GetStatusQrcOperationsResponse(source.Code, source.Message, source.Data?.Select(data => data.Convert()).ToArray());
        }

        public static Front.CreateQrcIdReservationResponse Convert(this Sbp.SbpNspkResponse<Sbp.CreateQrcIdReservationV1Response> source)
        {
            return new Front.CreateQrcIdReservationResponse(source.Code, source.Message, source.Data?.Convert());
        }

        public static Sbp.CreateCashRegisterQrRequest Convert(this Front.CreateCashRegisterQrRequest source, SbpSetting settings, string agentId)
        {
            return new Sbp.CreateCashRegisterQrRequest(
                agentId,
                settings.MemberId,
                settings.MerchantId,
                settings.Account,
                source.QrcId);
        }

        public static Front.ActivateCashRegisterQrResponse Convert(this Sbp.SbpNspkResponse<Sbp.CreateParamsResponse> source)
        {
            return new Front.ActivateCashRegisterQrResponse(source.Code, source.Message, source.Data?.Convert());
        }

        public static Front.GetCashRegQrStatusResponse Convert(this Sbp.SbpNspkResponse<Sbp.GetCashRegQrStatusResponse> source)
        {
            return new Front.GetCashRegQrStatusResponse(source.Code, source.Message, source.Data?.Convert());
        }

        public static Front.GetStatusCashRegQrOperationResponse Convert(this Sbp.SbpNspkResponse<Sbp.StatusCashRegQrResponse> source)
        {
            return new Front.GetStatusCashRegQrOperationResponse(source.Code, source.Message, source.Data?.Convert());
        }

        public static Sbp.CreateRefundRequest Convert(this Front.CreateRefundRequest source, SbpSetting settings)
        {
            return new Sbp.CreateRefundRequest(
                settings.MemberId,
                settings.MerchantId,
                source.OriginalQrcId,
                source.Amount,
                source.Currency,
                source.Kzo,
                source.AgentRefundRequestId);
        }

        public static Front.CreatedRefundResponse Convert(this Sbp.SbpNspkResponse<Sbp.CreatedRefundResponse> source)
        {
            return new Front.CreatedRefundResponse(source.Code, source.Message, source.Data?.Convert());
        }

        public static Front.GetRefundStatusResponse Convert(this Sbp.SbpNspkResponse<Sbp.RefundRequestStatusV2Response> source)
        {
            return new Front.GetRefundStatusResponse(source.Code, source.Message, source.Data?.Convert());
        }

        private static Front.GetCashRegQrStatusData Convert(this Sbp.GetCashRegQrStatusResponse source)
        {
            return new Front.GetCashRegQrStatusData(
                source.Status,
                source.ParamsId);
        }

        private static Front.GetStatusCashRegQrOperationData Convert(this Sbp.StatusCashRegQrResponse source)
        {
            return new Front.GetStatusCashRegQrOperationData(
                source.CashRegisterQrcStatus,
                source.TrxStatus,
                source.TrxId,
                source.Amount,
                source.Timestamp,
                source.PayerId,
                source.Kzo);
        }

        private static Front.GetRefundStatusData Convert(this Sbp.RefundRequestStatusV2Response source)
        {
            return new Front.GetRefundStatusData(
                source.OriginalQrcId,
                source.OriginalTrxId,
                source.RefundStatusCode,
                source.TrxId,
                source.TrxStatus,
                source.Amount,
                source.Timestamp,
                source.PayeeId,
                source.AgentRefundRequestId,
                source.OpkcRefundRequestId);
        }

        private static Front.CreatedRefundData Convert(this Sbp.CreatedRefundResponse source)
        {
            return new Front.CreatedRefundData(source.OpkcRefundRequestId);
        }

        private static Front.GetStatusQrcOperationsData Convert(this Sbp.GetStatusQRCOperationsResponse source)
        {
            return new Front.GetStatusQrcOperationsData(
                source.QrcId,
                source.Code,
                source.Message,
                source.Status,
                source.TrxId,
                source.Kzo);
        }

        private static Front.ActivateCashRegisterQrData Convert(this Sbp.CreateParamsResponse source)
        {
            return new Front.ActivateCashRegisterQrData(
                source.ParamsId,
                source.QrcId,
                source.Amount,
                source.Currency,
                source.PaymentPurpose,
                source.FraudScore);
        }

        private static Front.CreateQrcIdReservationData Convert(this Sbp.CreateQrcIdReservationV1Response source)
        {
            return new Front.CreateQrcIdReservationData(source.QrcIds);
        }

        private static Front.QrcPayload Convert(this Sbp.QrcPayloadResponse source)
        {
            return new Front.QrcPayload(source.QrcId, source.Payload, source.Status, source.Image?.Convert());
        }

        private static Front.Image Convert(this Sbp.Image source)
        {
            return new Front.Image(source.MediaType, source.Content);
        }
    }
}