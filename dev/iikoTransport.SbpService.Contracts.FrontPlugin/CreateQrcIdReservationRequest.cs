using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Запрос на получение массива идентификаторов многоразовых ссылок СБП для последующей регистрации ссылки с заданным идентификатором.
    /// </summary>
    [DataContract]
    public class CreateQrcIdReservationRequest
    {
        public CreateQrcIdReservationRequest(int quantity)
        {
            Quantity = quantity;
        }

        /// <summary>
        /// Количество идентификаторов для генерации
        /// </summary>
        [DataMember(IsRequired = true, Name = "quantity")]
        public int Quantity { get; }
    }
}