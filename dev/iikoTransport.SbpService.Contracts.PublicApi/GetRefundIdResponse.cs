using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Get refund id response.
    /// </summary>
    [DataContract]
    public class GetRefundIdResponse : SbpNspkBaseResponse<CreatedRefundData>
    {
        public GetRefundIdResponse(string code, string message, CreatedRefundData? data)
            : base(code, message, data)
        {
        }
    }
}