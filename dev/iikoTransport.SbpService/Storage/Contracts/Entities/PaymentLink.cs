using System;

namespace iikoTransport.SbpService.Storage.Contracts.Entities
{
    /// <summary>
    /// Информация о функциональных и кассовых платёжных ссылках СБП.
    /// </summary>
    public class PaymentLink
    {
        public PaymentLink(Guid id, string qrcId, string? paramsIdCol, Guid terminalGroupUocId, Guid? terminalId, DateTime updatedAt)
        {
            Id = id;
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
            ParamsId = paramsIdCol;
            TerminalGroupUocId = terminalGroupUocId;
            TerminalId = terminalId;
            UpdatedAt = updatedAt;
        }
        
        /// <summary>
        /// Уникальный идентификатор транспорта.
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// Идентификатор зарегистрированной Платежной ссылки СБП.
        /// </summary>
        public string QrcId { get; }
        
        /// <summary>
        /// Идентификатор активных значений параметров Платежной ссылки СБП. Передается, если оплата происходит по Кассовой ссылке СБП.
        /// </summary>
        public string? ParamsId { get; }

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

        public const string TableName = "payment_links";
        public const string IdCol = "id";
        public const string QrcIdCol = "qrc_id";
        public const string ParamsIdCol = "paramsId";
        public const string TerminalGroupUocIdCol = "terminal_group_uoc_id";
        public const string TerminalIdCol = "terminal_id";
        public const string UpdatedAtCol = "updated_at";
        
        public const string PrimaryKey = "payment_links_pkey";

        public static string AllFieldsWithAliases => string.Join(", ",
            $"{IdCol} {nameof(Id)}",
            $"{QrcIdCol} {nameof(QrcId)}",
            $"{ParamsIdCol} {nameof(ParamsId)}",
            $"{TerminalGroupUocIdCol} {nameof(TerminalGroupUocId)}",
            $"{TerminalIdCol} {nameof(TerminalId)}",
            $"{UpdatedAtCol} {nameof(UpdatedAt)}"
        );

        #endregion
    }
}