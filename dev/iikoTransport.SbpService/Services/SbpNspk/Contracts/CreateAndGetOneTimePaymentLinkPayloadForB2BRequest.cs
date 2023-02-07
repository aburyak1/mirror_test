﻿using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts
{
    /// <summary>
    /// Данные регистрируемой одноразовой Фукциональной ссылки СБП для B2B.
    /// </summary>
    [DataContract]
    public class CreateAndGetOneTimePaymentLinkPayloadForB2BRequest
    {
        public CreateAndGetOneTimePaymentLinkPayloadForB2BRequest(string agentId, string memberId, string account,
            string merchantId, string amount, string paymentPurpose, bool takeTax)
        {
            AgentId = agentId;
            MemberId = memberId;
            Account = account;
            MerchantId = merchantId;
            Amount = amount;
            PaymentPurpose = paymentPurpose;
            TakeTax = takeTax;
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
        /// Банковский счет ЮЛ или ИП
        /// </summary>
        [DataMember(IsRequired = true, Name = "account")]
        public string Account { get; }

        /// <summary>
        /// Идентификатор ТСП
        /// </summary>
        [DataMember(IsRequired = true, Name = "merchantId")]
        public string MerchantId { get; }

        /// <summary>
        /// Сумма Операции СБП C2B в копейках. Целое, положительное число. Валюта Операции СБП - рубли РФ.
        /// </summary>
        [DataMember(IsRequired = true, Name = "amount")]
        public string Amount { get; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        [DataMember(IsRequired = true, Name = "paymentPurpose")]
        public string PaymentPurpose { get; }

        /// <summary>
        /// Информация о взимании НДС. Допустимые значения:
        /// true – облагается НДС;
        /// false – не облагается НДС;
        /// </summary>
        [DataMember(IsRequired = true, Name = "takeTax")]
        public bool TakeTax { get; }
    }
}