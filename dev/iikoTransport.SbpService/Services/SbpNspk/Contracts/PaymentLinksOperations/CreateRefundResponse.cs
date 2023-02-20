using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations
{
    /// <summary>
    /// Ответ идентификатора ОПКЦ СБП запроса Агента ТСП на возврат по Операции СБП C2B.
    /// </summary>
    [DataContract]
    public class CreatedRefundResponse
    {
        public CreatedRefundResponse(string opkcRefundRequestId)
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