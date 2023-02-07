using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts
{
    /// <summary>
    /// Запрос содержимого для ранее зарегистрированной Функциональной ссылки СБП
    /// </summary>
    [DataContract]
    public class GetQRCPayloadRequest
    {
        public GetQRCPayloadRequest(string qrcId)
        {
            QrcId = qrcId;
        }

        /// <summary>
        /// Количество идентификаторов для генерации
        /// </summary>
        [DataMember(IsRequired = true, Name = "qrcId")]
        public string QrcId { get; }
    }
}