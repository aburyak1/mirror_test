using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.Merchants
{
    /// <summary>
    /// ТСП, зарегистрированное Банковским Агентом, и информация о Банке-Участнике.
    /// </summary>
    [DataContract]
    public class Member
    {
        public Member(string? memberId, string? memberName, Merchant[]? merchants)
        {
            MemberId = memberId;
            MemberName = memberName;
            Merchants = merchants;
        }

        /// <summary>
        /// Идентификатор Банка-Участника.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? MemberId { get; }

        /// <summary>
        /// Наименование Банка-Участника.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? MemberName { get; }

        /// <summary>
        /// ТСП, зарегистрированные Банковским Агентом.
        /// </summary>
        [DataMember(IsRequired = false)]
        public Merchant[]? Merchants { get; }
    }
}