using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Объект QR-кода.
    /// </summary>
    [DataContract]
    public class Image
    {
        public Image(string mediaType, string content)
        {
            MediaType = mediaType;
            Content = content;
        }

        /// <summary>
        /// Формат получаемого файла:
        /// </summary>
        /// <remarks>
        /// image/png - png; 
        /// image/svg+xml - xml of svg
        /// </remarks>
        [DataMember(IsRequired = true, Name = "mediaType")]
        public string MediaType { get; }

        /// <summary>
        /// base64encoded image. Формат для декодирования зависит от mediaType
        /// </summary>
        [DataMember(IsRequired = true, Name = "content")]
        public string Content { get; }
    }
}