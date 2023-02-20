using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Create reusable payment link request.
    /// </summary>
    [DataContract]
    public class CreateReusablePaymentLinkRequest
    {
        public CreateReusablePaymentLinkRequest(string agentId, string memberId, string account, string merchantId,
            string paymentPurpose, bool takeTax)
        {
            AgentId = agentId ?? throw new ArgumentNullException(nameof(agentId));
            MemberId = memberId ?? throw new ArgumentNullException(nameof(memberId));
            Account = account ?? throw new ArgumentNullException(nameof(account));
            MerchantId = merchantId ?? throw new ArgumentNullException(nameof(merchantId));
            PaymentPurpose = paymentPurpose ?? throw new ArgumentNullException(nameof(paymentPurpose));
            TakeTax = takeTax;
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
        /// Банковский счет ЮЛ или ИП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Account { get; }

        /// <summary>
        /// Идентификатор ТСП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string MerchantId { get; }

        /// <summary>
        /// Назначение платежа.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string PaymentPurpose { get; }

        /// <summary>
        /// Информация о взимании НДС. Допустимые значения:
        /// true – облагается НДС;
        /// false – не облагается НДС.
        /// </summary>
        [DataMember(IsRequired = true)]
        public bool TakeTax { get; }
    }
}