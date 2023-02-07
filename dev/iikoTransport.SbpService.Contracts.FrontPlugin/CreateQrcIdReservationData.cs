using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Данные ответа на получение массива идентификаторов многоразовых ссылок СБП для последующей регистрации ссылки с заданным идентификатором.
    /// </summary>
    [DataContract]
    public class CreateQrcIdReservationData
    {
        public CreateQrcIdReservationData(string[] qrcIds)
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