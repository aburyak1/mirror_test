using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Deactivate cash register QR response.
    /// </summary>
    [DataContract]
    public class DeactivateCashRegisterQrResponse: SbpNspkBaseResponse<object>
    {
        public DeactivateCashRegisterQrResponse(string code, string message, object? data)
            : base(code, message, data)
        {
        }
    }
}