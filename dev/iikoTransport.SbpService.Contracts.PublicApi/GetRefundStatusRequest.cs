using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Get refund status request.
    /// </summary>
    [DataContract]
    public class GetRefundStatusRequest
    {
        public GetRefundStatusRequest(string originalTrxId, string opkcRefundRequestId)
        {
            OriginalTrxId = originalTrxId ?? throw new ArgumentNullException(nameof(originalTrxId));
            OpkcRefundRequestId = opkcRefundRequestId ?? throw new ArgumentNullException(nameof(opkcRefundRequestId));
        }

        /// <summary>
        /// Идентификатор исходной Операции СБП C2B. 
        /// </summary>
        [DataMember(IsRequired = true)]
        public string OriginalTrxId { get; }

        /// <summary>
        /// Уникальный идентификатор запроса на возврат, назначенный ОПКЦ СБП. 
        /// </summary>
        [DataMember(IsRequired = true)]
        public string OpkcRefundRequestId { get; }
    }
}