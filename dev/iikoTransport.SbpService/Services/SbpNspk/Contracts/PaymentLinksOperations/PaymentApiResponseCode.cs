using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations
{
    /// <summary>
    /// Используемые транспортом коды результата обработки Payment API ОПКЦ СБП.
    /// </summary>
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PaymentApiResponseCode
    {
        /// <summary>
        /// Запрос обработан успешно. 
        /// </summary>
        [EnumMember]
        RQ00000
    }
}