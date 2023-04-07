using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Get refund status response data.
    /// </summary>
    [DataContract]
    public class GetRefundStatusData
    {
        public GetRefundStatusData(string? originalQrcId, string? originalTrxId, string? refundStatusCode, string? trxId, string? trxStatus,
            string? amount, string? timestamp, string? payeeId, string? agentRefundRequestId, string? opkcRefundRequestId)
        {
            OriginalQrcId = originalQrcId;
            OriginalTrxId = originalTrxId;
            RefundStatusCode = refundStatusCode;
            TrxId = trxId;
            TrxStatus = trxStatus;
            Amount = amount;
            Timestamp = timestamp;
            PayeeId = payeeId;
            AgentRefundRequestId = agentRefundRequestId;
            OpkcRefundRequestId = opkcRefundRequestId;
        }

        /// <summary>
        /// Идентификатор зарегистрированной Платежной ссылки СБП, по которому была выполнена исходная Операция СБП C2B.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? OriginalQrcId { get; }

        /// <summary>
        /// Идентификатор исходной Операции СБП C2B.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? OriginalTrxId { get; }

        /// <summary>
        /// Код ответа, соответствующий решению Банка Плательщика по запросу на возврат. 
        /// <remarks>Enum:"RQ00030" "RQ00031" "RQ05039" "RQ05040" "RQ05041" "RQ05052" "RQ07999".</remarks>
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? RefundStatusCode { get; }

        /// <summary>
        /// Идентификатор Операции СБП B2C.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? TrxId { get; }

        /// <summary>
        /// Статус Операции СБП B2C запроса на возврат:
        /// "NTST" – NOT_STARTED, Операция не начата;
        /// "RCVD" – RECEIVED, Операция в обработке;
        /// "ACWP" – ACCEPTED, Операция завершена успешно;
        /// "RJCT" – REJECTED, Операция отклонена.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? TrxStatus { get; }

        /// <summary>
        /// Сумма Операции СБП в копейках. Целое, положительное число. Валюта операции – рубли РФ.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? Amount { get; }

        /// <summary>
        /// Дата и время выполнения Операции СБП B2C.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? Timestamp { get; }

        /// <summary>
        /// Маскированный номер телефона Клиента-Получателя.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? PayeeId { get; }

        /// <summary>
        /// Уникальный идентификатор запроса, назначаемый ТСП или Агентом ТСП.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? AgentRefundRequestId { get; }

        /// <summary>
        /// Уникальный идентификатор запроса от ОПКЦ.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? OpkcRefundRequestId { get; }
    }
}