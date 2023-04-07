using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Получение списка ТСП, зарегистрированных для ЮЛ или ИП.
    /// </summary>
    [DataContract]
    public class SearchMerchantDataRequest
    {
        public SearchMerchantDataRequest(string ogrn, string bic)
        {
            Ogrn = ogrn ?? throw new ArgumentNullException(nameof(ogrn));
            Bic = bic ?? throw new ArgumentNullException(nameof(bic));
        }

        /// <summary>
        /// ОГРН ЮЛ или ИП. 
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Ogrn { get; }

        /// <summary>
        /// БИК банка-участника, в котором проводятся расчеты по операциям ТСП. 
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Bic { get; }
    }
}