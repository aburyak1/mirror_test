using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts
{
    /// <summary>
    /// Ответ на активацию Кассовой ссылки СБП для выполнения платежа.
    /// </summary>
    [DataContract]
    public class CreateParamsResponse
    {
        public CreateParamsResponse(string paramsId, string qrcId, string amount, string currency, string paymentPurpose, string fraudScore)
        {
            ParamsId = paramsId;
            QrcId = qrcId;
            Amount = amount;
            Currency = currency;
            PaymentPurpose = paymentPurpose;
            FraudScore = fraudScore;
        }

        /// <summary>
        /// Идентификатор активных значений параметров Кассовой ссылки СБП
        /// </summary>
        [DataMember(IsRequired = true, Name = "paramsId")]
        public string ParamsId { get; }

        /// <summary>
        /// Идентификатор зарегистрированной Кассовой ссылки СБП
        /// </summary>
        [DataMember(IsRequired = true, Name = "qrcId")]
        public string QrcId { get; }

        /// <summary>
        /// Сумма Операции СБП C2B в копейках. Целое, положительное число.
        /// </summary>
        [DataMember(IsRequired = true, Name = "amount")]
        public string Amount { get; }

        /// <summary>
        /// Валюта операции
        /// </summary>
        [DataMember(IsRequired = false, Name = "currency")]
        public string Currency { get; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        [DataMember(IsRequired = false, Name = "paymentPurpose")]
        public string PaymentPurpose { get; }

        /// <summary>
        /// Индикатор Подозрительной Операции Агента ТСП
        /// </summary>
        [DataMember(IsRequired = false, Name = "fraudScore")]
        public string FraudScore { get; }
    }
}