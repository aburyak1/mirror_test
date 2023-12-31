using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Created refund response.
    /// </summary>
    [DataContract]
    public class CreatedRefundResponse : SbpNspkBaseResponse<CreatedRefundData>
    {
        public CreatedRefundResponse(string code, string message, CreatedRefundData? data)
            : base(code, message, data)
        {
        }
    }
}