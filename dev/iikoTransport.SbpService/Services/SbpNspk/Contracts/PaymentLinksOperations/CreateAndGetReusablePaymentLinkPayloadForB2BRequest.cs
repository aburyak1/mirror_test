using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations
{
    /// <summary>
    /// Данные регистрируемой многоразовой Фукциональной ссылки СБП для B2B.
    /// </summary>
    [DataContract]
    public class CreateAndGetReusablePaymentLinkPayloadForB2BRequest
    {
        public CreateAndGetReusablePaymentLinkPayloadForB2BRequest(string agentId, string memberId, string account, string merchantId, string? amount,
            string paymentPurpose, bool takeTax, string? totalTaxAmount)
        {
            if (takeTax && string.IsNullOrWhiteSpace(totalTaxAmount))
                throw new ArgumentNullException(nameof(totalTaxAmount));
            
            if(takeTax && string.IsNullOrWhiteSpace(amount))
                throw new ArgumentNullException(nameof(amount));
            
            AgentId = agentId ?? throw new ArgumentNullException(nameof(agentId));
            MemberId = memberId ?? throw new ArgumentNullException(nameof(memberId));
            Account = account ?? throw new ArgumentNullException(nameof(account));
            MerchantId = merchantId ?? throw new ArgumentNullException(nameof(merchantId));
            Amount = amount;
            PaymentPurpose = paymentPurpose ?? throw new ArgumentNullException(nameof(paymentPurpose));
            TakeTax = takeTax;
            TotalTaxAmount = totalTaxAmount;
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
        /// Сумма Операции СБП C2B в копейках. Целое, положительное число. Валюта Операции СБП - рубли РФ.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? Amount { get; }

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

        /// <summary>
        /// Сумма НДС в копейках. Валюта НДС - рубли РФ. Условия заполнения в зависимости от значения поля takeTax:
        /// takeTax=FALSE: totalTaxAmount должно отсутствовать;
        /// takeTax=TRUE: Сумма НДС должна быть указана в параметре totalTaxAmount.
        /// При заполненном поле totalTaxAmount, поле amount также должно быть заполнено.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string? TotalTaxAmount { get; }
    }
}