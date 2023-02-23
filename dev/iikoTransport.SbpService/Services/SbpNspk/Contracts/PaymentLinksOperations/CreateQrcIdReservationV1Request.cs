using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Services.SbpNspk.Contracts.PaymentLinksOperations
{
    /// <summary>
    /// Запрос на получение массива идентификаторов многоразовых ссылок СБП для последующей регистрации ссылки с заданным идентификатором.
    /// </summary>
    [DataContract]
    public class CreateQrcIdReservationV1Request
    {
        public CreateQrcIdReservationV1Request(int quantity)
        {
            Quantity = quantity;
        }

        /// <summary>
        /// Количество идентификаторов для генерации.
        /// </summary>
        [DataMember(IsRequired = true)]
        public int Quantity { get; }
    }
}