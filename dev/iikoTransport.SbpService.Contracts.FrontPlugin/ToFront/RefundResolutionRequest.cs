using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.ToFront
{
    /// <summary>
    /// Refund resolution event.
    /// </summary>
    [DataContract]
    public class RefundResolutionRequest
    {
        public RefundResolutionRequest(string code, string message, string status, string originalTrxId, string agentRefundRequestId,
            string opkcRefundRequestId)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            OriginalTrxId = originalTrxId ?? throw new ArgumentNullException(nameof(originalTrxId));
            AgentRefundRequestId = agentRefundRequestId ?? throw new ArgumentNullException(nameof(agentRefundRequestId));
            OpkcRefundRequestId = opkcRefundRequestId ?? throw new ArgumentNullException(nameof(opkcRefundRequestId));
        }

        /// <summary>
        /// Код ответа:
        ///
        /// "RQ00030" – Согласие Банка Плательщика на выполнение Операции СБП B2C по сценарию "Возврат по Операции СБП C2B";
        /// "RQ05039" – Отказ от Банка Плательщика. Не найдена исходная Операция СБП C2B;
        /// "RQ05040" – Отказ от Банка Плательщика. Сумма возврата/возвратов превышает сумму исходной Операции СБП C2B;
        /// "RQ05041" – Длительное поручение не найдено в Банке Плательщика;
        /// "RQ07999" – Техническая ошибка.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Code { get; }

        /// <summary>
        /// Описание кода ответа.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Message { get; }

        /// <summary>
        /// Статус обработки запроса на возврат средств.
        /// <remarks>Enum: "ACWP", "RJCT".</remarks>
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Status { get; }

        /// <summary>
        /// Идентификатор исходной Операции СБП C2B.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string OriginalTrxId { get; }

        /// <summary>
        /// Уникальный идентификатор запроса, назначаемый ТСП или Агентом ТСП для однозначной
        /// идентификации запроса на стороне Банка Плательщика, ТСП и Агента ТСП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string AgentRefundRequestId { get; }

        /// <summary>
        /// Уникальный идентификатор запроса, назначаемый ОПКЦ СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string OpkcRefundRequestId { get; }
    }
}