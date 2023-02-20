using System;

namespace iikoTransport.SbpService.Storage.Contracts.Entities
{
    /// <summary>
    /// Информация о функциональных и кассовых платёжных ссылках СБП.
    /// </summary>
    public class PaymentLink
    {
        public PaymentLink(string qrcId, Guid terminalGroupUocId, DateTime updatedAt)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
            TerminalGroupUocId = terminalGroupUocId;
            UpdatedAt = updatedAt;
        }
        
        /// <summary>
        /// Идентификатор зарегистрированной Платежной ссылки СБП.
        /// </summary>
        public string QrcId { get; }

        /// <summary>
        /// Идентификатор терминальной группы в ЕЦО.
        /// </summary>
        public Guid TerminalGroupUocId { get; }

        /// <summary>
        /// Дата-время последнего изменения заявки (UTC).
        /// </summary>
        public DateTime UpdatedAt { get; }

        #region Db details

        public const string TableName = "payment_links";
        public const string QrcIdCol = "qrc_id";
        public const string TerminalGroupUocIdCol = "terminal_group_uoc_id";
        public const string UpdatedAtCol = "updated_at";
        
        public const string PrimaryKey = "payment_links_pkey";

        public static string AllFields => string.Join(", ",
            QrcIdCol,
            TerminalGroupUocIdCol,
            UpdatedAtCol
        );

        public static string AllFieldsWithAliases => string.Join(", ",
            $"{QrcIdCol} {nameof(QrcId)}",
            $"{TerminalGroupUocIdCol} {nameof(TerminalGroupUocId)}",
            $"{UpdatedAtCol} {nameof(UpdatedAt)}"
        );

        #endregion
    }
}