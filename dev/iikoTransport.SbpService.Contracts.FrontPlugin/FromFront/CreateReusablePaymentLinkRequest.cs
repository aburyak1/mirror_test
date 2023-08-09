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
        public CreateReusablePaymentLinkRequest(string? amount, string? paymentPurpose, 
            string? mediaType = null, int? width = null, int? height = null)
        {
            Amount = amount;
            PaymentPurpose = paymentPurpose;
            MediaType = mediaType;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Сумма Операции СБП C2B в копейках. Целое, положительное число. Валюта Операции СБП - рубли РФ.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? Amount { get; }

        /// <summary>
        /// Назначение платежа.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? PaymentPurpose { get; }

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