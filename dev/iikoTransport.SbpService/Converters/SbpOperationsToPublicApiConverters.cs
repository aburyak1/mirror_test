using System.Linq;
using iikoTransport.SbpService.Storage.Contracts.Entities;
using PA = iikoTransport.SbpService.Contracts.PublicApi;
using Sbp = iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations;

namespace iikoTransport.SbpService.Converters.PublicApi
{
    /// <summary>
    /// Converters from sbp.nspk contracts to front plugin contracts.
    /// </summary>
    public static class SbpOperationsToPublicApiConverters
    {
        public static PA.PaymentLinkPayloadResponse Convert(this Sbp.SbpNspkResponse<Sbp.QrcPayloadResponse> source)
        {
            return new PA.PaymentLinkPayloadResponse(source.Code, source.Message, source.Data?.Convert());
        }

        public static Sbp.CreateAndGetOneTimePaymentLinkPayloadForB2BRequest Convert(
            this PA.CreateOneTimePaymentLinkRequest source, SbpSetting settings, string agentId)
        {
            return new Sbp.CreateAndGetOneTimePaymentLinkPayloadForB2BRequest(
                agentId,
                settings.MemberId,
                settings.Account,
                settings.MerchantId,
                source.Amount.ToString(),
                source.QrTtl,
                source.PaymentPurpose,
                source.TakeTax,
                source.TotalTaxAmount);
        }

        public static PA.GetStatusQrcOperationsResponse Convert(this Sbp.SbpNspkResponse<Sbp.GetStatusQRCOperationsResponse[]> source)
        {
            return new PA.GetStatusQrcOperationsResponse(source.Code, source.Message, source.Data?.Select(data => data.Convert()).ToArray());
        }

        public static Sbp.CreateRefundRequest Convert(this PA.CreateRefundRequest source, SbpSetting settings)
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

        public static PA.CreatedRefundResponse Convert(this Sbp.SbpNspkResponse<Sbp.CreatedRefundResponse> source)
        {
            return new PA.CreatedRefundResponse(source.Code, source.Message, source.Data?.Convert());
        }

        public static PA.GetRefundStatusResponse Convert(this Sbp.SbpNspkResponse<Sbp.RefundRequestStatusV2Response> source)
        {
            return new PA.GetRefundStatusResponse(source.Code, source.Message, source.Data?.Convert());
        }

        public static PA.SetNewAccountResponse Convert(this Sbp.SbpNspkResponse<Sbp.SetNewAccountResponse> source)
        {
            return new PA.SetNewAccountResponse(source.Code, source.Message, source.Data?.Convert());
        }

        public static PA.SearchMerchantDataResponse Convert(this Sbp.SbpNspkResponse<iikoTransport.SbpService.Services.SbpNspk.Contracts.Merchants.SearchMerchantDataResponse> source)
        {
            return new PA.SearchMerchantDataResponse(source.Code, source.Message, source.Data?.Convert());
        }

        private static PA.SetNewAccountData Convert(this Sbp.SetNewAccountResponse source)
        {
            return new PA.SetNewAccountData(source.QrcId, source.Status, source.Account);
        }

        private static PA.GetRefundStatusData Convert(this Sbp.RefundRequestStatusV2Response source)
        {
            return new PA.GetRefundStatusData(
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

        private static PA.CreatedRefundData Convert(this Sbp.CreatedRefundResponse source)
        {
            return new PA.CreatedRefundData(source.OpkcRefundRequestId);
        }

        private static PA.GetStatusQrcOperationsData Convert(this Sbp.GetStatusQRCOperationsResponse source)
        {
            return new PA.GetStatusQrcOperationsData(
                source.QrcId,
                source.Code,
                source.Message,
                source.Status,
                source.TrxId,
                source.Kzo);
        }

        private static PA.QrcPayload Convert(this Sbp.QrcPayloadResponse source)
        {
            return new PA.QrcPayload(source.QrcId, source.Payload, source.Status, source.Image?.Convert());
        }

        private static PA.Image Convert(this Sbp.Image source)
        {
            return new PA.Image(source.MediaType, source.Content);
        }

        private static PA.SearchMerchantData Convert(this iikoTransport.SbpService.Services.SbpNspk.Contracts.Merchants.SearchMerchantDataResponse source)
        {
            return new PA.SearchMerchantData(
                source.LegalName,
                source.Members?.Select(member => member.Convert()).ToArray());
        }

        private static PA.Member Convert(this iikoTransport.SbpService.Services.SbpNspk.Contracts.Merchants.Member source)
        {
            return new PA.Member(
                source.MemberId,
                source.MemberName,
                source.Merchants?.Select(merchant => merchant.Convert()).ToArray());
        }

        private static PA.Merchant Convert(this iikoTransport.SbpService.Services.SbpNspk.Contracts.Merchants.Merchant source)
        {
            return new PA.Merchant(
                source.MerchantId,
                source.BrandName,
                source.Mcc,
                source.Address,
                source.RegistrationDate);
        }
    }
}