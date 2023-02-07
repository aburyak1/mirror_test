using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Запрос статуса Операций СБП по идентификаторам QR
    /// </summary>
    [DataContract]
    public class GetStatusQRCOperationsRequest
    {
        public GetStatusQRCOperationsRequest(string[] qrcIds)
        {
            QrcIds = qrcIds;
        }

        /// <summary>
        /// Массив qrcId, по которым нужно получить статус Операций СБП C2B
        /// </summary>
        [DataMember(IsRequired = true, Name = "qrcIds")]
        public string[] QrcIds { get; }
    }
}