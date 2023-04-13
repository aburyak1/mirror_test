using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Данные успешного ответа на запрос метода "Изменение счета ЮЛ, ИП или самозанятого для зарегистрированной Кассовой ссылки СБП".
    /// </summary>
    [DataContract]
    public class SetNewAccountData
    {
        public SetNewAccountData(string qrcId, string status, string account)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            Account = account ?? throw new ArgumentNullException(nameof(account));
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