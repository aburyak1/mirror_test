using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Payment link payload.
    /// </summary>
    [DataContract]
    public class PaymentLinkPayloadResponse : SbpNspkBaseResponse<QrcPayload>
    {
        public PaymentLinkPayloadResponse(string code, string message, QrcPayload? data)
            : base(code, message, data)
        {
        }
    }
}