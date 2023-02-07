using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Ответ на деактивацию Кассовой ссылки СБП для выполнения платежа.
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