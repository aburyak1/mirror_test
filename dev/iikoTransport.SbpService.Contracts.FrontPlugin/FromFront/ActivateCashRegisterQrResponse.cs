using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Activate cash register QR response.
    /// </summary>
    [DataContract]
    public class ActivateCashRegisterQrResponse: SbpNspkBaseResponse<ActivateCashRegisterQrData>
    {
        public ActivateCashRegisterQrResponse(string code, string message, ActivateCashRegisterQrData? data)
            : base(code, message, data)
        {
        }
    }
}