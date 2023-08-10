using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations
{
    /// <summary>
    /// Данные регистрируемой одноразовой Фукциональной ссылки СБП для B2B.
    /// </summary>
    [DataContract]
    public class CreateQRCRequest
    {
        public CreateQRCRequest(string agentId, string memberId, string account, string merchantId, string qrcType,
            string? amount, int? qrTtl, string? paymentPurpose)
        {
            AgentId = agentId ?? throw new ArgumentNullException(nameof(agentId));
            MemberId = memberId ?? throw new ArgumentNullException(nameof(memberId));
            Account = account ?? throw new ArgumentNullException(nameof(account));
            MerchantId = merchantId ?? throw new ArgumentNullException(nameof(merchantId));
            TemplateVersion = "01";
            QrcType = qrcType;
            Amount = amount;
            Currency = string.IsNullOrEmpty(Amount) ? null : "RUB";
            QrTtl = qrTtl;
            PaymentPurpose = paymentPurpose;
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
        /// Банковский счет ЮЛ, ИП или самозанятого. Поле обязательно для заполнения, если qrcType = "01" или qrcType = "02".
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Account { get; }

        /// <summary>
        /// Идентификатор ТСП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string MerchantId { get; }

        /// <summary>
        /// Версия payload:
        /// "01" - Версия 1
        /// </summary>
        [DataMember(IsRequired = true)]
        public string TemplateVersion { get; }

        /// <summary>
        /// Тип ссылки СБП:
        /// "01" - QR-Static (Многоразовая Платежная ссылка СБП). Может использоваться для выполнения множества Операций СБП C2B
        /// "02" - QR-Dynamic (Одноразовая Платежная ссылка СБП). Предназначена для выполнения единичной Операции СБП C2B.
        /// Используется в Сценариях "Оплата с привязкой счета" и "Оплата с привязанного счета"
        /// "03" - QR-Subscription (Информационная ссылка СБП для привязки счета Плательщика)
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcType { get; }

        /// <summary>
        /// Сумма Операции СБП C2B в копейках. Целое, положительное число. Обязателен для заполнения, если qrcType = "02".
        /// Всегда отсутствует для qrcType = "03". Может отсутствовать при регистрации qrcType = 01.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? Amount { get; }

        /// <summary>
        /// Валюта операции:
        /// RUB - Российский рубль
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? Currency { get; }

        /// <summary>
        /// Срок жизни Функциональной ссылки СБП в минутах. Необязательное поле. Минимальное допустимое значение - "1".
        /// Максимальное допустимое значение - "129 600" (90 дней).
        /// Внимание! Рекомендация ОПКЦ СБП: устанавливать срок жизни Функциональной ссылки СБП не менее "5" минут.
        /// Если поле "qrTtl" не передано в запросе, будет использовано значение по умолчанию - "4 320" минут (3 суток).
        /// Неприменимо для qrcType = "01".
        /// </summary>
        [DataMember(IsRequired = false)]
        public int? QrTtl { get; }

        /// <summary>
        /// Назначение платежа. Обязательно к заполнению в сценарии "Оплата с привязанного счета".
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? PaymentPurpose { get; }
    }
}