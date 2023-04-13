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
        public CreateReusablePaymentLinkRequest(string paymentPurpose, bool takeTax, string? amount, string? totalTaxAmount = null,
            string? mediaType = null, int? width = null, int? height = null)
        {
            if (takeTax)
            {
                if (string.IsNullOrWhiteSpace(totalTaxAmount))
                    throw new ArgumentException($"{nameof(totalTaxAmount)} must be set when {nameof(takeTax)} is true. ", nameof(totalTaxAmount));
                if (string.IsNullOrWhiteSpace(amount))
                    throw new ArgumentException($"{nameof(amount)} must be set when {nameof(takeTax)} is true. ", nameof(amount));
            }

            PaymentPurpose = paymentPurpose ?? throw new ArgumentNullException(nameof(paymentPurpose));
            TakeTax = takeTax;
            Amount = amount;
            TotalTaxAmount = totalTaxAmount;
            MediaType = mediaType;
            Width = width;
            Height = height;
        }

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
        /// Сумма Операции СБП C2B в копейках. Целое, положительное число. Валюта Операции СБП - рубли РФ.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? Amount { get; }

        /// <summary>
        /// Сумма НДС в копейках. Валюта НДС - рубли РФ. Условия заполнения в зависимости от значения поля takeTax:
        /// takeTax=FALSE: totalTaxAmount должно отсутствовать;
        /// takeTax=TRUE: Сумма НДС должна быть указана в параметре totalTaxAmount.
        /// При заполненном поле totalTaxAmount, поле amount также должно быть заполнено.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string? TotalTaxAmount { get; }

        /// <summary>
        /// Опциональное получение QR-кода для Функциональной ссылки СБП.
        /// "image/png"
        /// "image/svg+xml"
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? MediaType { get; }

        /// <summary>
        /// Ширина изображения.
        /// <remarks>Default: 300</remarks>
        /// </summary>
        [DataMember(IsRequired = false)]
        public int? Width { get; }

        /// <summary>
        /// Высота изображения.
        /// <remarks>Default: 300</remarks>
        /// </summary>
        [DataMember(IsRequired = false)]
        public int? Height { get; }
    }
}