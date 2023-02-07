using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Данные регистрируемой одноразовой Фукциональной ссылки СБП для B2B.
    /// </summary>
    [DataContract]
    public class CreateOneTimePaymentLinkRequest
    {
        public CreateOneTimePaymentLinkRequest(int amount, string paymentPurpose, bool takeTax)
        {
            Amount = amount;
            PaymentPurpose = paymentPurpose;
            TakeTax = takeTax;
        }

        /// <summary>
        /// Сумма Операции СБП C2B в копейках. Целое, положительное число. Валюта Операции СБП - рубли РФ.
        /// </summary>
        [DataMember(IsRequired = true, Name = "amount")]
        public int Amount { get; }

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