using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations
{
    /// <summary>
    /// Ответ на получение массива идентификаторов многоразовых ссылок СБП для последующей регистрации ссылки с заданным идентификатором.
    /// </summary>
    [DataContract]
    public class CreateQrcIdReservationV1Response
    {
        public CreateQrcIdReservationV1Response(string[] qrcIds)
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