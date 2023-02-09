using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Reservate QR-code IDs for reusable payment links request.
    /// </summary>
    [DataContract]
    public class CreateQrcIdReservationRequest
    {
        public CreateQrcIdReservationRequest(int quantity)
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