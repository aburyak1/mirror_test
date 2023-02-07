using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts
{
    /// <summary>
    /// Запрос на создание Кассовой ссылки СБП, поддерживающей логику работы с Корзинами клиента.
    /// </summary>
    [DataContract]
    public class CreateCashRegisterQrRequest
    {
        public CreateCashRegisterQrRequest(string agentId, string memberId, string merchantId,
            string account, string qrcId)
        {
            AgentId = agentId;
            MemberId = memberId;
            MerchantId = merchantId;
            Account = account;
            QrcId = qrcId;
        }

        /// <summary>
        /// Идентификатор Агента ТСП
        /// </summary>
        [DataMember(IsRequired = true, Name = "agentId")]
        public string AgentId { get; }

        /// <summary>
        /// Идентификатор Банка Получателя
        /// </summary>
        [DataMember(IsRequired = true, Name = "memberId")]
        public string MemberId { get; }

        /// <summary>
        /// Идентификатор зарегистрированного ТСП в СБП
        /// </summary>
        [DataMember(IsRequired = true, Name = "merchantId")]
        public string MerchantId { get; }

        /// <summary>
        /// Банковский счет ЮЛ, ИП или самозанятого
        /// </summary>
        [DataMember(IsRequired = true, Name = "account")]
        public string Account { get; }

        /// <summary>
        /// Идентификатор многоразовой Платежной ссылки, предварительно полученный в запросе "Получение идентификаторов для многоразовых ссылок СБП".
        /// </summary>
        [DataMember(IsRequired = false, Name = "qrcId")]
        public string QrcId { get; }
    }
}