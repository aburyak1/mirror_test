using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Запрос статуса Операций СБП по идентификаторам QR
    /// </summary>
    [DataContract]
    public class GetRefundStatusRequest
    {
        public GetRefundStatusRequest(string originalTrxId, string opkcRefundRequestId)
        {
            OriginalTrxId = originalTrxId;
            OpkcRefundRequestId = opkcRefundRequestId;
        }

        /// <summary>
        /// Идентификатор исходной Операции СБП C2B. 
        /// </summary>
        [DataMember(IsRequired = true, Name = "originalTrxId")]
        public string OriginalTrxId { get; }

        /// <summary>
        /// Уникальный идентификатор запроса на возврат, назначенный ОПКЦ СБП. 
        /// </summary>
        [DataMember(IsRequired = true, Name = "opkcRefundRequestId")]
        public string OpkcRefundRequestId { get; }
    }
}