using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Ответ на запрос "Получение списка ТСП, зарегистрированных для ЮЛ или ИП".
    /// </summary>
    [DataContract]
    public class SearchMerchantDataResponse : SbpNspkBaseResponse<SearchMerchantData>
    {
        public SearchMerchantDataResponse(string code, string message, SearchMerchantData? data)
            : base(code, message, data)
        {
        }
    }
}