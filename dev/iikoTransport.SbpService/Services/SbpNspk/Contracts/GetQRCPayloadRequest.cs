using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts
{
    /// <summary>
    /// Запрос содержимого для ранее зарегистрированной Функциональной ссылки СБП.
    /// </summary>
    [DataContract]
    public class GetQRCPayloadRequest
    {
        public GetQRCPayloadRequest(string qrcId)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
        }

        /// <summary>
        /// Количество идентификаторов для генерации.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }
    }
}