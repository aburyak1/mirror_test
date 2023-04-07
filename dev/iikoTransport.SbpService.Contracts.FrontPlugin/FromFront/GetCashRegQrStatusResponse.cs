using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Get cash register QR status response.
    /// </summary>
    [DataContract]
    public class GetCashRegQrStatusResponse: SbpNspkBaseResponse<GetCashRegQrStatusData>
    {
        public GetCashRegQrStatusResponse(string code, string message, GetCashRegQrStatusData? data)
            : base(code, message, data)
        {
        }
    }
}