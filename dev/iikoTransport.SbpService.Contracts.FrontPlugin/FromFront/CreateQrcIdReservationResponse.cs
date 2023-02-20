using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin.FromFront
{
    /// <summary>
    /// Reservate QR-code IDs for reusable payment links response.
    /// </summary>
    [DataContract]
    public class CreateQrcIdReservationResponse: SbpNspkBaseResponse<CreateQrcIdReservationData>
    {
        public CreateQrcIdReservationResponse(string code, string message, CreateQrcIdReservationData? data)
            : base(code, message, data)
        {
        }
    }
}