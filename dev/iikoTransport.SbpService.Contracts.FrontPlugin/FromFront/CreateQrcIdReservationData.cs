using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Reservate QR-code IDs for reusable payment links response data.
    /// </summary>
    [DataContract]
    public class CreateQrcIdReservationData
    {
        public CreateQrcIdReservationData(string[] qrcIds)
        {
            QrcIds = qrcIds ?? throw new ArgumentNullException(nameof(qrcIds));
        }

        /// <summary>
        /// Массив сгенерированных идентификаторов Платежной ссылки СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string[] QrcIds { get; }
    }
}