using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Get refund status response.
    /// </summary>
    [DataContract]
    public class GetRefundStatusResponse : SbpNspkBaseResponse<GetRefundStatusData>
    {
        public GetRefundStatusResponse(string code, string message, GetRefundStatusData? data)
            : base(code, message, data)
        {
        }
    }
}