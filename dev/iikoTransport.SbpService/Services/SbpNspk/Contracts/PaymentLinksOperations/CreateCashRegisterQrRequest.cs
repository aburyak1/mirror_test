using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations
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
            AgentId = agentId ?? throw new ArgumentNullException(nameof(agentId));
            MemberId = memberId ?? throw new ArgumentNullException(nameof(memberId));
            MerchantId = merchantId ?? throw new ArgumentNullException(nameof(merchantId));
            Account = account ?? throw new ArgumentNullException(nameof(account));
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
        }

        /// <summary>
        /// Идентификатор Агента ТСП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string AgentId { get; }

        /// <summary>
        /// Идентификатор Банка Получателя.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string MemberId { get; }

        /// <summary>
        /// Идентификатор зарегистрированного ТСП в СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string MerchantId { get; }

        /// <summary>
        /// Банковский счет ЮЛ, ИП или самозанятого.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Account { get; }

        /// <summary>
        /// Идентификатор многоразовой Платежной ссылки, предварительно полученный в запросе "Получение идентификаторов для многоразовых ссылок СБП".
        /// </summary>
        [DataMember(IsRequired = false)]
        public string QrcId { get; }
    }
}