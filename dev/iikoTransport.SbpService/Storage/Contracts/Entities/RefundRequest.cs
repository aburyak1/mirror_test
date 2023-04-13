using System;

namespace iikoTransport.SbpService.Storage.Contracts.Entities
{
    /// <summary>
    /// Информация о запросах на возврат по Операции СБП C2B.
    /// </summary>
    public class RefundRequest
    {
        public RefundRequest(Guid id, string opkcRefundRequestId, string trxId, Guid terminalGroupUocId, Guid? terminalId, DateTime updatedAt)
        {
            Id = id;
            OpkcRefundRequestId = opkcRefundRequestId ?? throw new ArgumentNullException(nameof(opkcRefundRequestId));
            TrxId = trxId ?? throw new ArgumentNullException(nameof(trxId));
            TerminalGroupUocId = terminalGroupUocId;
            TerminalId = terminalId;
            UpdatedAt = updatedAt;
        }
        
        /// <summary>
        /// Уникальный идентификатор транспорта.
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// Уникальный идентификатор запроса на возврат, назначенный ОПКЦ СБП.
        /// </summary>
        public string OpkcRefundRequestId { get; }
        
        /// <summary>
        /// Идентификатор исходной Операции СБП C2B.
        /// </summary>
        public string TrxId { get; }

        /// <summary>
        /// Идентификатор терминальной группы в ЕЦО.
        /// </summary>
        public Guid TerminalGroupUocId { get; }

        /// <summary>
        /// Идентификатор терминала в RMS.
        /// Должен быть заполнен, если фронтовый плагин СБП установлен на терминал,
        /// отличный от того, на который установлен фронтовый плагин транспорта.
        /// </summary>
        public Guid? TerminalId { get; }

        /// <summary>
        /// Дата-время последнего изменения заявки (UTC).
        /// </summary>
        public DateTime UpdatedAt { get; }

        #region Db details

        public const string TableName = "refund_requests";
        public const string IdCol = "id";
        public const string OpkcRefundRequestIdCol = "opkc_refund_request_id";
        public const string TrxIdCol = "trx_id";
        public const string TerminalGroupUocIdCol = "terminal_group_uoc_id";
        public const string TerminalIdCol = "terminal_id";
        public const string UpdatedAtCol = "updated_at";
        
        public const string PrimaryKey = "refund_requests_pkey";

        public static string AllFieldsWithAliases => string.Join(", ",
            $"{IdCol} {nameof(Id)}",
            $"{OpkcRefundRequestIdCol} {nameof(OpkcRefundRequestId)}",
            $"{TrxIdCol} {nameof(TrxId)}",
            $"{TerminalGroupUocIdCol} {nameof(TerminalGroupUocId)}",
            $"{TerminalIdCol} {nameof(TerminalId)}",
            $"{UpdatedAtCol} {nameof(UpdatedAt)}"
        );

        #endregion
    }
}