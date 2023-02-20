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
        public CreateOneTimePaymentLinkRequest(int amount, string paymentPurpose, bool takeTax)
        {
            Amount = amount;
            PaymentPurpose = paymentPurpose ?? throw new ArgumentNullException(nameof(paymentPurpose));
            TakeTax = takeTax;
        }

        /// <summary>
        /// Сумма Операции СБП C2B в копейках. Целое, положительное число. Валюта Операции СБП - рубли РФ.
        /// </summary>
        [DataMember(IsRequired = true)]
        public int Amount { get; }

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