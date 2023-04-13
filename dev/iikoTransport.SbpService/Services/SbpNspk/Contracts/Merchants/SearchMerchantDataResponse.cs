using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.Merchants
{
    /// <summary>
    /// Данные успешного ответа на запрос "Получение списка ТСП, зарегистрированных для ЮЛ или ИП".
    /// </summary>
    [DataContract]
    public class SearchMerchantDataResponse
    {
        public SearchMerchantDataResponse(string? legalName, Member[]? members)
        {
            LegalName = legalName;
            Members = members;
        }

        /// <summary>
        /// Наименование ЮЛ, ИП.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? LegalName { get; }

        /// <summary>
        /// Список Банков-Участников и ТСП, зарегистрированных их Банковскими Агентами.
        /// </summary>
        [DataMember(IsRequired = false)]
        public Member[]? Members { get; }
    }
}