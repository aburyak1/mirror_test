using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Payment link payload data. 
    /// </summary>
    [DataContract]
    public class QrcPayload
    {
        public QrcPayload(string qrcId, string payload, string status, Image? image)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
            Payload = payload ?? throw new ArgumentNullException(nameof(payload));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            Image = image;
        }

        /// <summary>
        /// Идентификатор зарегистрированной ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }

        /// <summary>
        /// Payload зарегистрированной Платежной или Информационной или Кассовой ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Payload { get; }
        
        /// <summary>
        /// Статус регистрации Платежной или Информационной или Кассовой ссылки СБП.
        /// </summary>
        /// <remarks>Value:"CREATED"</remarks>
        [DataMember(IsRequired = true)]
        public string Status { get; }

        /// <summary>
        /// Объект QR-кода.
        /// </summary>
        [DataMember(IsRequired = false)]
        public Image? Image { get; }
    }
}