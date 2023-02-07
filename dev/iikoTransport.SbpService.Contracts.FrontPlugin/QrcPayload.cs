using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    [DataContract]
    public class QrcPayload
    {
        /// <summary>
        /// Данные регистрируемой одноразовой Фукциональной ссылки СБП для B2B
        /// </summary>
        public QrcPayload(string qrcId, string payload, string status, Image? image)
        {
            QrcId = qrcId;
            Payload = payload;
            Status = status;
            Image = image;
        }

        /// <summary>
        /// Идентификатор зарегистрированной ссылки СБП
        /// </summary>
        [DataMember(IsRequired = true, Name = "qrcId")]
        public string QrcId { get; }

        /// <summary>
        /// Payload зарегистрированной Платежной или Информационной или Кассовой ссылки СБП
        /// </summary>
        [DataMember(IsRequired = true, Name = "payload")]
        public string Payload { get; }
        
        /// <summary>
        /// Статус регистрации Платежной или Информационной или Кассовой ссылки СБП
        /// </summary>
        /// <remarks>Value:"CREATED"</remarks>
        [DataMember(IsRequired = true, Name = "status")]
        public string Status { get; }

        /// <summary>
        /// Объект QR-кода
        /// </summary>
        [DataMember(IsRequired = false, Name = "image")]
        public Image? Image { get; }
    }
}