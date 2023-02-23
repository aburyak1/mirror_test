using System;

namespace iikoTransport.SbpService.Storage.Contracts.Entities
{
    /// <summary>
    /// Информация о запросах на возврат по Операции СБП C2B.
    /// </summary>
    public class RefundRequest
    {
        public RefundRequest(string opkcRefundRequestId, string trxId, Guid terminalGroupUocId)
        {
            OpkcRefundRequestId = opkcRefundRequestId ?? throw new ArgumentNullException(nameof(opkcRefundRequestId));
            TrxId = trxId ?? throw new ArgumentNullException(nameof(trxId));
            TerminalGroupUocId = terminalGroupUocId;
        }
        
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

        #region Db details

        public const string TableName = "refund_requests";
        public const string OpkcRefundRequestIdCol = "opkc_refund_request_id";
        public const string TrxIdCol = "trx_id";
        public const string TerminalGroupUocIdCol = "terminal_group_uoc_id";
        
        public const string PrimaryKey = "refund_requests_pkey";

        public static string AllFields => string.Join(", ",
            OpkcRefundRequestIdCol,
            TrxIdCol,
            TerminalGroupUocIdCol
        );

        public static string AllFieldsWithAliases => string.Join(", ",
            $"{OpkcRefundRequestIdCol} {nameof(OpkcRefundRequestId)}",
            $"{TrxIdCol} {nameof(TrxId)}",
            $"{TerminalGroupUocIdCol} {nameof(TerminalGroupUocId)}"
        );

        #endregion
    }
}