using Front = iikoTransport.SbpService.Contracts.FrontPlugin.ToFront;
using Sbp = iikoTransport.SbpService.Services.SbpNspk.Contracts.Events;

namespace iikoTransport.SbpService.Converters
{
    /// <summary>
    /// Converters from sbp envents contracts to front plugin contracts.
    /// </summary>
    public static class SbpEventsToFrontConverters
    {
        public static Front.RefundResolutionRequest Convert(this Sbp.RefundResolutionRequest source)
        {
            return new Front.RefundResolutionRequest(
                source.Code,
                source.Message,
                source.Status,
                source.OriginalTrxId,
                source.AgentRefundRequestId,
                source.OpkcRefundRequestId);
        }
        
        public static Front.SendFinalStatusAckRequest Convert(this Sbp.SendFinalStatusAckV2Request source)
        {
            return new Front.SendFinalStatusAckRequest(
                source.QrcId,
                source.TrxId,
                source.ParamsId,
                source.Status,
                source.Amount,
                source.ErrorCode,
                source.Message,
                source.Timestamp,
                source.PayerId,
                source.Kzo);
        }
        
        public static Front.SendFinalStatusB2CAckRequest Convert(this Sbp.SendFinalStatusB2CAckRequest source)
        {
            return new Front.SendFinalStatusB2CAckRequest(
                source.OriginalQrcId,
                source.OriginalTrxId,
                source.TrxId,
                source.Status,
                source.Amount,
                source.ErrorCode,
                source.Message,
                source.Timestamp,
                source.PayeeId,
                source.AgentRefundRequestId,
                source.OpkcRefundRequestId);
        }
    }
}