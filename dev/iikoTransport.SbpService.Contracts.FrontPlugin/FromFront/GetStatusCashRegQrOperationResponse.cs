using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Get status of cash register QR operation response.
    /// </summary>
    [DataContract]
    public class GetStatusCashRegQrOperationResponse : SbpNspkBaseResponse<GetStatusCashRegQrOperationData>
    {
        public GetStatusCashRegQrOperationResponse(string code, string message, GetStatusCashRegQrOperationData? data)
            : base(code, message, data)
        {
        }
    }
}