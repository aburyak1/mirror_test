using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Ответ на активацию Кассовой ссылки СБП для выполнения платежа.
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