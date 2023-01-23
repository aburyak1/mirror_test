using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts
{
    /// <summary>
    /// Данные регистрируемой одноразовой Фукциональной ссылки СБП для B2B.
    /// </summary>
    [DataContract]
    public class CreateAndGetOneTimePaymentLinkPayloadForB2BRequest
    {
        public CreateAndGetOneTimePaymentLinkPayloadForB2BRequest(string agentId, string memberId, string account,
            string merchantId, string amount, string paymentPurpose, bool takeTax)
        {
            AgentId = agentId;
            MemberId = memberId;
            Account = account;
            MerchantId = merchantId;
            Amount = amount;
            PaymentPurpose = paymentPurpose;
            TakeTax = takeTax;
        }

        [DataMember(IsRequired = true, Name = "agentId")]
        public string AgentId { get; set; }

        [DataMember(IsRequired = true, Name = "memberId")]
        public string MemberId { get; set; }

        [DataMember(IsRequired = true, Name = "account")]
        public string Account { get; set; }

        [DataMember(IsRequired = true, Name = "merchantId")]
        public string MerchantId { get; set; }

        [DataMember(IsRequired = true, Name = "amount")]
        public string Amount { get; set; }

        [DataMember(IsRequired = true, Name = "paymentPurpose")]
        public string PaymentPurpose { get; set; }

        [DataMember(IsRequired = true, Name = "takeTax")]
        public bool TakeTax { get; set; }
    }
}