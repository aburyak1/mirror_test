using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.PublicApi
{
    /// <summary>
    /// Created refund data.
    /// </summary>
    [DataContract]
    public class CreatedRefundData
    {
        public CreatedRefundData(string opkcRefundRequestId)
        {
            OpkcRefundRequestId = opkcRefundRequestId ?? throw new ArgumentNullException(nameof(opkcRefundRequestId));
        }

        /// <summary>
        /// Уникальный идентификатор запроса на возврат, назначенный ОПКЦ СБП.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string OpkcRefundRequestId { get; }
    }
}