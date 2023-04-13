using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Ответ на запрос метода "Изменение счета ЮЛ, ИП или самозанятого для зарегистрированной Кассовой ссылки СБП".
    /// </summary>
    [DataContract]
    public class SetNewAccountResponse : SbpNspkBaseResponse<SetNewAccountData>
    {
        public SetNewAccountResponse(string code, string message, SetNewAccountData? data)
            : base(code, message, data)
        {
        }
    }
}