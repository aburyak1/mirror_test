using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Ответ на запрос данных ТСП.
    /// </summary>
    [DataContract]
    public class GetMerchantDataResponse : SbpNspkBaseResponse<GetMerchantData>
    {
        public GetMerchantDataResponse(string code, string message, GetMerchantData? data)
            : base(code, message, data)
        {
        }
    }
}