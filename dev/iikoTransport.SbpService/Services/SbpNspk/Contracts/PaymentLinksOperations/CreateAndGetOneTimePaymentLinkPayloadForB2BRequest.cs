using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations
{
    /// <summary>
    /// Данные регистрируемой одноразовой Фукциональной ссылки СБП для B2B.
    /// </summary>
    [DataContract]
    public class CreateAndGetOneTimePaymentLinkPayloadForB2BRequest
    {
        public CreateAndGetOneTimePaymentLinkPayloadForB2BRequest(string agentId, string memberId, string account, string merchantId, string amount,
            int? qrTtl, string paymentPurpose, bool takeTax, string? totalTaxAmount)
        {
            if (takeTax && string.IsNullOrWhiteSpace(totalTaxAmount))
                throw new NullReferenceException(nameof(totalTaxAmount));
            
            AgentId = agentId ?? throw new ArgumentNullException(nameof(agentId));
            MemberId = memberId ?? throw new ArgumentNullException(nameof(memberId));
            Account = account ?? throw new ArgumentNullException(nameof(account));
            MerchantId = merchantId ?? throw new ArgumentNullException(nameof(merchantId));
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            QrTtl = qrTtl;
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
        [DataMember(IsRequired = true)]
        public string Amount { get; }

        /// <summary>
        /// Срок жизни Функциональной ссылки СБП B2B в минутах. Минимальное допустимое значение - 1.
        /// Максимальное допустимое значение - 129600 (90 дней). Если поле "qrTtl" не передано в запросе,
        /// будет использовано значение по умолчанию - 4320 минут (3 суток).
        /// </summary>
        [DataMember(IsRequired = false)]
        public int? QrTtl { get; }

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
        /// totalTaxAmount всегда отсутствует при takeTax=FALSE;
        /// totalTaxAmount всегда присутствует при takeTax=TRUE. 
        /// </summary>
        [DataMember(IsRequired = true)]
        public string? TotalTaxAmount { get; }
    }
}