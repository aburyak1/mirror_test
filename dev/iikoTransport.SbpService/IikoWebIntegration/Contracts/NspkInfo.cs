using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.IikoWebIntegration.Contracts
{
    /// <summary>
    /// Record describing connected NSPK info.
    /// </summary>
    [DataContract]
    public class NspkInfo
    {
        public NspkInfo(Guid id, Guid terminalGroupUocId, string merchantId, string account, string memberId, bool deleted)
        {
            Id = id;
            TerminalGroupUocId = terminalGroupUocId;
            MerchantId = merchantId ?? throw new ArgumentNullException(nameof(merchantId));
            Account = account ?? throw new ArgumentNullException(nameof(account));
            MemberId = memberId ?? throw new ArgumentNullException(nameof(memberId));
            Deleted = deleted;
        }

        /// <summary>
        /// Sbp setting record id. Unique through all terminal groups.
        /// </summary>  
        [DataMember(IsRequired = true)]
        public Guid Id { get; }

        /// <summary>
        /// Terminal group UOC id this record belongs to.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Guid TerminalGroupUocId { get; }

        /// <summary>
        /// Merchant Id in the SBP system.
        /// </summary>
        [DataMember(Name = "merchantId", IsRequired = true)]
        public string MerchantId { get; }

        /// <summary>
        /// Bank account.
        /// </summary>
        [DataMember(Name = "account", IsRequired = true)]
        public string Account { get; }

        /// <summary>
        /// Bank Id.
        /// </summary>
        [DataMember(Name = "memberId", IsRequired = true)]
        public string MemberId { get; }

        /// <summary>
        /// Deleted flag.
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool Deleted { get; }
    }
}