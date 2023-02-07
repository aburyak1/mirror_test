using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts
{
    /// <summary>
    /// qrcId по которым необходимо получить статусы операций
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