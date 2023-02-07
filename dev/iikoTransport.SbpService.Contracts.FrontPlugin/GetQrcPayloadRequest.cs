using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Данные ранее зарегистрированной Функциональной ссылки СБП.
    /// </summary>
    [DataContract]
    public class GetQrcPayloadRequest
    {
        public GetQrcPayloadRequest(string qrcId)
        {
            QrcId = qrcId;
        }

        /// <summary>
        /// Идентификатор зарегистрированной Функциональной ссылки СБП. 
        /// </summary>
        [DataMember(IsRequired = true, Name = "qrcId")]
        public string QrcId { get; }
    }
}