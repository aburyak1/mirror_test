using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.ToFront
{
    /// <summary>
    /// Final status c2b ack event.
    /// </summary>
    [DataContract]
    public class SendFinalStatusAckRequest
    {
        public SendFinalStatusAckRequest(string qrcId, string trxId, string paramsId, string status, int amount, string errorCode, string message,
            string timestamp, string payerId, string kzo)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
            TrxId = trxId ?? throw new ArgumentNullException(nameof(trxId));
            ParamsId = paramsId ?? throw new ArgumentNullException(nameof(paramsId));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            Amount = amount;
            ErrorCode = errorCode ?? throw new ArgumentNullException(nameof(errorCode));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Timestamp = timestamp ?? throw new ArgumentNullException(nameof(timestamp));
            PayerId = payerId ?? throw new ArgumentNullException(nameof(payerId));
            Kzo = kzo ?? throw new ArgumentNullException(nameof(kzo));
        }

        /// <summary>
        /// Идентификатор зарегистрированной Платежной ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }

        /// <summary>
        /// Идентификатор Операции СБП C2B.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string TrxId { get; }

        /// <summary>
        /// Идентификатор активных значений параметров Платежной ссылки СБП. Передается, если оплата происходила по Кассовой ссылке СБП.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string ParamsId { get; }

        /// <summary>
        /// Финальный статус Операции СБП C2B:
        ///
        /// "ACWP" - ACCEPTED операция завершена успешно;
        /// "RJCT" - REJECTED операция отклонена.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Status { get; }

        /// <summary>
        /// Сумма Операции СБП C2B в копейках. Целое, положительное число.
        /// </summary>
        [DataMember(IsRequired = true)]
        public int Amount { get; }

        /// <summary>
        /// Зарезервированное поле для будущего использования.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string ErrorCode { get; }

        /// <summary>
        /// Зарезервированное поле для будущего использования.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Message { get; }

        /// <summary>
        /// Дата и время выполнения Операции СБП C2B.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Timestamp { get; }

        /// <summary>
        /// Маскированный номер телефона Клиента-Плательщика.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string PayerId { get; }

        /// <summary>
        /// Контрольное Значение Операции СБП. Формируется ОПКЦ СБП на основании собственных
        /// алгоритмов. Присутствует при условии, что Операция СБП C2B по запрашиваемому qrcId
        /// завершена успешно (параметр "status" ="ACWP").
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Kzo { get; }
    }
}