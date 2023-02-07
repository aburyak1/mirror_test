using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts
{
    /// <summary>
    /// Ответ на получение массива идентификаторов многоразовых ссылок СБП для последующей регистрации ссылки с заданным идентификатором.
    /// </summary>
    [DataContract]
    public class CreateQrcIdReservationV1Response
    {
        public CreateQrcIdReservationV1Response(string[] qrcIds)
        {
            QrcIds = qrcIds;
        }

        /// <summary>
        /// Массив сгенерированных идентификаторов Платежной ссылки СБП
        /// </summary>
        [DataMember(IsRequired = true, Name = "qrcIds")]
        public string[] QrcIds { get; }
    }
}