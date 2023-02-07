using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Ответ на запрос Агента ТСП на возврат по Операции СБП C2B.
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