using System;

namespace iikoTransport.SbpService.Storage.Contracts.Entities
{
    /// <summary>
    /// Информация о настройках клиентов сбп из веба.
    /// </summary>
    public class SbpSetting
    {
        public SbpSetting(Guid id, Guid terminalGroupUocId, string merchantId, string account, string memberId)
        {
            Id = id;
            TerminalGroupUocId = terminalGroupUocId;
            MerchantId = merchantId;
            Account = account;
            MemberId = memberId;
        }

        /// <summary>
        /// Идентификатор настройки.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Идентификатор терминальной группы в UOC.
        /// </summary>
        public Guid TerminalGroupUocId { get; }

        /// <summary>
        /// Идентификатор ТСП.
        /// </summary>
        public string MerchantId { get; }

        /// <summary>
        /// Банковский счет ЮЛ или ИП.
        /// </summary>
        public string Account { get; }

        /// <summary>
        /// Идентификатор Банка Получателя.
        /// </summary>
        public string MemberId { get; }

        #region Db details

        public const string TableName = "sbp_settings";
        public const string IdCol = "id";
        public const string TerminalGroupUocIdCol = "terminal_group_uoc_id";
        public const string MerchantIdCol = "merchantId";
        public const string AccountCol = "account";
        public const string MemberIdCol = "memberId";
        
        public const string PrimaryKey = "sbp_settings_pkey";

        public static string AllFields => string.Join(", ",
            IdCol,
            TerminalGroupUocIdCol,
            MerchantIdCol,
            AccountCol,
            MemberIdCol
        );

        public static string AllFieldsWithAliases => string.Join(", ",
            $"{IdCol} {nameof(Id)}",
            $"{TerminalGroupUocIdCol} {nameof(TerminalGroupUocId)}",
            $"{MerchantIdCol} {nameof(MerchantId)}",
            $"{AccountCol} {nameof(Account)}",
            $"{MemberIdCol} {nameof(MemberId)}"
        );

        #endregion
    }
}