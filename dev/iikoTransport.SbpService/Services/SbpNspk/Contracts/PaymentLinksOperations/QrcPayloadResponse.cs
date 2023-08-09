using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations
{
    [DataContract]
    public class QrcPayloadResponse
    {
        /// <summary>
        /// Данные регистрируемой Фукциональной ссылки СБП.
        /// </summary>
        public QrcPayloadResponse(string qrcId, string payload, string status, Image? image)
        {
            QrcId = qrcId ?? throw new ArgumentNullException(nameof(qrcId));
            Payload = payload ?? throw new ArgumentNullException(nameof(payload));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            Image = image;
        }

        /// <summary>
        /// Идентификатор зарегистрированной Платежной или Информационной ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QrcId { get; }

        /// <summary>
        /// Payload зарегистрированной Платежной или Информационной ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Payload { get; }
        
        /// <summary>
        /// Статус регистрации Платежной или Информационной ссылки СБП.
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