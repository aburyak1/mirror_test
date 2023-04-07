using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Get QR-code payload for existing payment link request.
    /// </summary>
    [DataContract]
    public class GetQrcPayloadRequest
    {
        public GetQrcPayloadRequest(string qrcId, string? mediaType = null, int? width = null, int? height = null)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
            MediaType = mediaType;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Идентификатор зарегистрированной Функциональной ссылки СБП. 
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }

        /// <summary>
        /// Опциональное получение QR-кода для Функциональной ссылки СБП.
        /// "image/png"
        /// "image/svg+xml"
        /// </summary>
        [DataMember(IsRequired = false)]
        public string? MediaType { get; }

        /// <summary>
        /// Ширина изображения.
        /// Default: 300
        /// </summary>
        [DataMember(IsRequired = false)]
        public int? Width { get; }

        /// <summary>
        /// Высота изображения.
        /// Default: 300
        /// </summary>
        [DataMember(IsRequired = false)]
        public int? Height { get; }
    }
}