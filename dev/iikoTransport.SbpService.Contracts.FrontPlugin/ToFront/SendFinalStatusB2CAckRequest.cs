using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.ToFront
{
    /// <summary>
    /// Final status b2c ack event.
    /// </summary>
    [DataContract]
    public class SendFinalStatusB2CAckRequest
    {
        public SendFinalStatusB2CAckRequest(string originalQrcId, string originalTrxId, string trxId, string status, int amount, string errorCode,
            string message, string timestamp, string payeeId, string agentRefundRequestId, string opkcRefundRequestId)
        {
            OriginalQrcId = originalQrcId ?? throw new ArgumentNullException(nameof(originalQrcId));
            OriginalTrxId = originalTrxId ?? throw new ArgumentNullException(nameof(originalTrxId));
            TrxId = trxId ?? throw new ArgumentNullException(nameof(trxId));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            Amount = amount;
            ErrorCode = errorCode ?? throw new ArgumentNullException(nameof(errorCode));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Timestamp = timestamp ?? throw new ArgumentNullException(nameof(timestamp));
            PayeeId = payeeId ?? throw new ArgumentNullException(nameof(payeeId));
            AgentRefundRequestId = agentRefundRequestId ?? throw new ArgumentNullException(nameof(agentRefundRequestId));
            OpkcRefundRequestId = opkcRefundRequestId ?? throw new ArgumentNullException(nameof(opkcRefundRequestId));
        }

        /// <summary>
        /// Идентификатор зарегистрированной Платежной ссылки СБП, по которому была выполнена исходная Операция СБП C2B.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string OriginalQrcId { get; }

        /// <summary>
        /// Идентификатор исходной Операции СБП C2B.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string OriginalTrxId { get; }

        /// <summary>
        /// Идентификатор Операции СБП B2C по сценарию «Возврат по Операции СБП C2B».
        /// </summary>
        [DataMember(IsRequired = true)]
        public string TrxId { get; }

        /// <summary>
        /// Финальный статус Операции СБП B2C:
        ///
        /// "ACWP" - ACCEPTED операция завершена успешно;
        /// "RJCT" - REJECTED операция отклонена.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Status { get; }

        /// <summary>
        /// Сумма Операции СБП в копейках. Целое, положительное число. Валюта операции – рубли РФ.
        /// </summary>
        [DataMember(IsRequired = true)]
        public int Amount { get; }

        /// <summary>
        /// Зарезервированное поле для будущего использования.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string ErrorCode { get; }

        /// <summary>
        /// Зарезервированное поле для будущего использования.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Message { get; }

        /// <summary>
        /// Дата и время выполнения Операции СБП B2C.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Timestamp { get; }

        /// <summary>
        /// Маскированный номер телефона Клиента-Получателя (Плательщика в исходной Операции C2B, кому делается возврат).
        /// </summary>
        [DataMember(IsRequired = true)]
        public string PayeeId { get; }

        /// <summary>
        /// Уникальный идентификатор запроса на возврат от Агента ТСП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string AgentRefundRequestId { get; }

        /// <summary>
        /// Уникальный идентификатор запроса на возврат от ОПКЦ СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string OpkcRefundRequestId { get; }
    }
}