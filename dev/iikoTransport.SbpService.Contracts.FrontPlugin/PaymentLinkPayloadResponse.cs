using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Ответ запроса регистрации Фукциональной ссылки СБП для B2B.
    /// </summary>
    [DataContract]
    public class PaymentLinkPayloadResponse: SbpNspkBaseResponse<QrcPayload>
    {
        public PaymentLinkPayloadResponse(string code, string message, QrcPayload? data)
            : base(code, message, data)
        {
        }
    }
}