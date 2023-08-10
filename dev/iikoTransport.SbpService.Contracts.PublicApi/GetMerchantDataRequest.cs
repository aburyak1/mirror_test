using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Запрос данных ТСП.
    /// </summary>
    [DataContract]
    public class GetMerchantDataRequest
    {
        public GetMerchantDataRequest(string merchantId)
        {
            MerchantId = merchantId ?? throw new ArgumentNullException(nameof(merchantId));
        }

        /// <summary>
        /// Идентификатор ТСП в СБП. 
        /// </summary>
        [DataMember(IsRequired = true)]
        public string MerchantId { get; }
    }
}