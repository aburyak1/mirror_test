using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations
{
    /// <summary>
    /// Ответ на активацию Кассовой ссылки СБП для выполнения платежа.
    /// </summary>
    [DataContract]
    public class CreateParamsResponse
    {
        public CreateParamsResponse(string paramsId, string qrcId, string amount, string currency, string? paymentPurpose, string? fraudScore)
        {
            ParamsId = paramsId ?? throw new ArgumentNullException(nameof(paramsId));
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
            PaymentPurpose = paymentPurpose;
            FraudScore = fraudScore;
        }

        /// <summary>
        /// Идентификатор активных значений параметров Кассовой ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string ParamsId { get; }

        /// <summary>
        /// Идентификатор зарегистрированной Кассовой ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }

        /// <summary>
        /// Сумма Операции СБП C2B в копейках. Целое, положительное число.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Amount { get; }

        /// <summary>
        /// Валюта операции.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Currency { get; }

        /// <summary>
        /// Назначение платежа.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? PaymentPurpose { get; }

        /// <summary>
        /// Индикатор Подозрительной Операции Агента ТСП.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? FraudScore { get; }
    }
}