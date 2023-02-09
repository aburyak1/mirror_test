using System.Linq;
using iikoTransport.SbpService.Storage.Contracts.Entities;
using Front = iikoTransport.SbpService.Contracts.FrontPlugin;
using Sbp = iikoTransport.SbpService.Services.SbpNspk.Contracts;

namespace iikoTransport.SbpService.Converters
{
    /// <summary>
    /// Converters from sbp.nspk contracts to front plugin contracts.
    /// </summary>
    public static class SbpToFrontPluginConverters
    {
        public static Front.PaymentLinkPayloadResponse Convert(this Sbp.SbpNspkResponse<Sbp.QrcPayloadResponse> source)
        {
            return new Front.PaymentLinkPayloadResponse(source.Code, source.Message, source.Data?.Convert());
        }

        public static Sbp.CreateAndGetOneTimePaymentLinkPayloadForB2BRequest Convert(
            this Front.CreateOneTimePaymentLinkRequest source, SbpSetting settings, string agentId)
        {
            return new Sbp.CreateAndGetOneTimePaymentLinkPayloadForB2BRequest(
                agentId,
                settings.MemberId,
                settings.Account,
                settings.MerchantId,
                source.Amount.ToString(),
                source.PaymentPurpose,
                source.TakeTax);
        }

        public static Sbp.CreateAndGetReusablePaymentLinkPayloadForB2BRequest Convert(
            this Front.CreateReusablePaymentLinkRequest source, SbpSetting settings, string agentId)
        {
            return new Sbp.CreateAndGetReusablePaymentLinkPayloadForB2BRequest(
                agentId,
                settings.MemberId,
                settings.Account,
                settings.MerchantId,
                source.PaymentPurpose,
                source.TakeTax);
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
                settings.Account,
                settings.MerchantId,
                source.QrcId);
        }

        public static Front.ActivateCashRegisterQrResponse Convert(this Sbp.SbpNspkResponse<Sbp.CreateParamsResponse> source)
        {
            return new Front.ActivateCashRegisterQrResponse(source.Code, source.Message, source.Data?.Convert());
        }

        public static Front.GetStatusQrcOperationsResponse Convert(this Sbp.SbpNspkResponse<Sbp.GetStatusQRCOperationsResponse[]> source)
        {
            return new Front.GetStatusQrcOperationsResponse(source.Code, source.Message, source.Data?.Select(data => data.Convert()).ToArray());
        }

        public static Sbp.CreatePaymentPetitionRequest Convert(this Front.CreatePaymentPetitionRequest source, SbpSetting settings)
        {
            return new Sbp.CreatePaymentPetitionRequest(
                settings.MemberId,
                settings.MerchantId,
                source.OriginalQrcId,
                source.Amount,
                source.Currency,
                source.Kzo,
                source.AgentRefundRequestId);
        }

        public static Front.CreatePaymentPetitionResponse Convert(this Sbp.SbpNspkResponse<Sbp.CreatePaymentPetitionResponse> source)
        {
            return new Front.CreatePaymentPetitionResponse(source.Code, source.Message, source.Data?.Convert());
        }

        public static Front.GetRefundStatusResponse Convert(this Sbp.SbpNspkResponse<Sbp.RefundRequestStatusV2Response> source)
        {
            return new Front.GetRefundStatusResponse(source.Code, source.Message, source.Data?.Convert());
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

        private static Front.CreatePaymentPetitionData Convert(this Sbp.CreatePaymentPetitionResponse source)
        {
            return new Front.CreatePaymentPetitionData(source.OpkcRefundRequestId);
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