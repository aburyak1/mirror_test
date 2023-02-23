using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Create refund request.
    /// </summary>
    [DataContract]
    public class CreateRefundRequest
    {
        public CreateRefundRequest(string trxId, string originalQrcId, string amount, string currency, string kzo,
            string agentRefundRequestId)
        {
            TrxId = trxId ?? throw new ArgumentNullException(nameof(trxId));
            OriginalQrcId = originalQrcId ?? throw new ArgumentNullException(nameof(originalQrcId));
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
            Kzo = kzo ?? throw new ArgumentNullException(nameof(kzo));
            AgentRefundRequestId = agentRefundRequestId ?? throw new ArgumentNullException(nameof(agentRefundRequestId));
        }

        /// <summary>
        /// Идентификатор исходной Операции СБП C2B. 
        /// </summary>
        [DataMember(IsRequired = true)]
        public string TrxId { get; }

        /// <summary>
        /// Идентификатор зарегистрированной Платежной ссылки СБП, по которой выполнена исходная Операция СБП C2B.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string OriginalQrcId { get; }

        /// <summary>
        /// Сумма возврата в копейках.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Amount { get; }

        /// <summary>
        /// Валюта операции.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Currency { get; }

        /// <summary>
        /// Контрольное Значение Операции СБП, полученное в "Уведомлении для Агента ТСП о финальном статусе по Операции СБП C2B от ОПКЦ СБП".
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Kzo { get; }

        /// <summary>
        /// Уникальный идентификатор запроса, назначаемый ТСП или Агентом ТСП для однозначной
        /// идентификации запроса на стороне Банка Плательщика, ТСП и Агента ТСП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string AgentRefundRequestId { get; }
    }
}