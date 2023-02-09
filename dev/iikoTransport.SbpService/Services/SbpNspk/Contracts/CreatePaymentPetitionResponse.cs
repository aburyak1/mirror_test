using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts
{
    /// <summary>
    /// Ответ на запрос Агента ТСП на возврат по Операции СБП C2B.
    /// </summary>
    [DataContract]
    public class CreatePaymentPetitionResponse
    {
        public CreatePaymentPetitionResponse(string opkcRefundRequestId)
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