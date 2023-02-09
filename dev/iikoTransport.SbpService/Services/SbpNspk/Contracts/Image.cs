using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts
{
    /// <summary>
    /// Данные регистрируемой одноразовой Фукциональной ссылки СБП для B2B.
    /// </summary>
    [DataContract]
    public class Image
    {
        public Image(string mediaType, string content)
        {
            MediaType = mediaType ?? throw new ArgumentNullException(nameof(mediaType));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        /// <summary>
        /// Формат получаемого файла.
        /// </summary>
        /// <remarks>
        /// image/png - png; 
        /// image/svg+xml - xml of svg
        /// </remarks>
        [DataMember(IsRequired = true)]
        public string MediaType { get; }

        /// <summary>
        /// base64encoded image. Формат для декодирования зависит от mediaType.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Content { get; }
    }
}