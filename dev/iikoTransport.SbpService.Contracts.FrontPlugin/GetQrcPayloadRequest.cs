using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Get QR-code payload for existing payment link request.
    /// </summary>
    [DataContract]
    public class GetQrcPayloadRequest
    {
        public GetQrcPayloadRequest(string qrcId)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
        }

        /// <summary>
        /// Идентификатор зарегистрированной Функциональной ссылки СБП. 
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }
    }
}