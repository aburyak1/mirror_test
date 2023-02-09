using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Create payment petition response data.
    /// </summary>
    [DataContract]
    public class CreatePaymentPetitionData
    {
        public CreatePaymentPetitionData(string opkcRefundRequestId)
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