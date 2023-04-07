using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.Merchants
{
    /// <summary>
    /// Информация о ТСП.
    /// </summary>
    [DataContract]
    public class Merchant
    {
        public Merchant(string? merchantId, string? brandName, string? mcc, string? address, string? registrationDate)
        {
            MerchantId = merchantId;
            BrandName = brandName;
            Mcc = mcc;
            Address = address;
            RegistrationDate = registrationDate;
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

        /// <summary>
        /// Дата регистрации ТСП.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? RegistrationDate { get; }
    }
}