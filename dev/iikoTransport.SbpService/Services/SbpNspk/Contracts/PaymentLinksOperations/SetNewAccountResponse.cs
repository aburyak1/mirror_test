using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations
{
    /// <summary>
    /// Данные успешного ответа на запрос метода "Изменение счета ЮЛ, ИП или самозанятого для зарегистрированной Кассовой ссылки СБП".
    /// </summary>
    [DataContract]
    public class SetNewAccountResponse
    {
        public SetNewAccountResponse(string qrcId, string status, string account)
        {
            QrcId = qrcId;
            Status = status;
            Account = account;
        }

        /// <summary>
        /// Идентификатор зарегистрированной Кассовой ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }

        /// <summary>
        /// Статус изменения счета в СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Status { get; }

        /// <summary>
        /// Счет ЮЛ, ИП или самозанятого. 
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Account { get; }
    }
}