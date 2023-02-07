using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts
{
    /// <summary>
    /// Запрос Агента ТСП на возврат по Операции СБП C2B.
    /// </summary>
    [DataContract]
    public class CreatePaymentPetitionRequest
    {
        public CreatePaymentPetitionRequest(string memberId, string merchantId, string originalQrcId, string amount, string currency, string kzo,
            string agentRefundRequestId)
        {
            MemberId = memberId;
            MerchantId = merchantId;
            OriginalQrcId = originalQrcId;
            Amount = amount;
            Currency = currency;
            Kzo = kzo;
            AgentRefundRequestId = agentRefundRequestId;
        }

        /// <summary>
        /// Идентификатор Банка Плательщика, который будет выполнять возврат денежных средств по запросу Агента ТСП
        /// </summary>
        [DataMember(IsRequired = true, Name = "memberId")]
        public string MemberId { get; }

        /// <summary>
        /// Идентификатор ТСП, инициировавшего запрос на возврат средств
        /// </summary>
        [DataMember(IsRequired = true, Name = "merchantId")]
        public string MerchantId { get; }

        /// <summary>
        /// Идентификатор зарегистрированной Платежной ссылки СБП, по которой выполнена исходная Операция СБП C2B
        /// </summary>
        [DataMember(IsRequired = true, Name = "originalQrcId")]
        public string OriginalQrcId { get; }

        /// <summary>
        /// Сумма возврата в копейках
        /// </summary>
        [DataMember(IsRequired = true, Name = "amount")]
        public string Amount { get; }

        /// <summary>
        /// Валюта операции
        /// </summary>
        [DataMember(IsRequired = true, Name = "currency")]
        public string Currency { get; }

        /// <summary>
        /// Контрольное Значение Операции СБП, полученное в "Уведомлении для Агента ТСП о финальном статусе по Операции СБП C2B от ОПКЦ СБП"
        /// </summary>
        [DataMember(IsRequired = true, Name = "kzo")]
        public string Kzo { get; }

        /// <summary>
        /// Уникальный идентификатор запроса, назначаемый ТСП или Агентом ТСП для однозначной
        /// идентификации запроса на стороне Банка Плательщика, ТСП и Агента ТСП
        /// </summary>
        [DataMember(IsRequired = true, Name = "agentRefundRequestId")]
        public string AgentRefundRequestId { get; }
    }
}