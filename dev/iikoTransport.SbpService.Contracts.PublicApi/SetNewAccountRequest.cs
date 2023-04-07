using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Изменение счета ЮЛ, ИП или самозанятого для зарегистрированной Кассовой ссылки СБП.
    /// </summary>
    [DataContract]
    public class SetNewAccountRequest
    {
        public SetNewAccountRequest(string qrcId, string account)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
            Account = account ?? throw new ArgumentNullException(nameof(account));
        }

        /// <summary>
        /// Идентификатор зарегистрированной Кассовой ссылки СБП. 
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }

        /// <summary>
        /// Счет ЮЛ, ИП или самозанятого. 
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Account { get; }
    }
}