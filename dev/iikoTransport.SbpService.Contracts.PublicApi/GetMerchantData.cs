using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Данные успешного ответа на "Запрос данных ТСП".
    /// </summary>
    [DataContract]
    public class GetMerchantData
    {
        public GetMerchantData(string? merchantId, string? brandName, string? mcc, string? address)
        {
            MerchantId = merchantId;
            BrandName = brandName;
            Mcc = mcc;
            Address = address;
        }

        /// <summary>
        /// Идентификатор ТСП в СБП.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? MerchantId { get; }

        /// <summary>
        /// Название ТСП (имя по вывеске).
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? BrandName { get; }

        /// <summary>
        /// MCC код.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? Mcc { get; }

        /// <summary>
        /// Адрес ТСП.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? Address { get; }
    }
}