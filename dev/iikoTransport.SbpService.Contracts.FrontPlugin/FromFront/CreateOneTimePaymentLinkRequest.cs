using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Create one-time payment link request.
    /// </summary>
    [DataContract]
    public class CreateOneTimePaymentLinkRequest
    {
        public CreateOneTimePaymentLinkRequest(string amount, string? paymentPurpose, int? qrTtl = null, 
            string? mediaType = null, int? width = null, int? height = null)
        {
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            PaymentPurpose = paymentPurpose;
            QrTtl = qrTtl;
            MediaType = mediaType;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Сумма Операции СБП C2B в копейках. Целое, положительное число. Валюта Операции СБП - рубли РФ.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Amount { get; }

        /// <summary>
        /// Назначение платежа.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? PaymentPurpose { get; }

        /// <summary>
        /// Срок жизни Функциональной ссылки СБП B2B в минутах. Минимальное допустимое значение - 1.
        /// Максимальное допустимое значение - 129600 (90 дней). Если поле "qrTtl" не передано в запросе,
        /// будет использовано значение по умолчанию - 4320 минут (3 суток).
        /// Рекомендация ОПКЦ СБП: устанавливать срок жизни Функциональной ссылки СБП не менее "5" минут.
        /// </summary>
        [DataMember(IsRequired = false)]
        public int? QrTtl { get; }

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