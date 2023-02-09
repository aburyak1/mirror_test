using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Create payment petition response.
    /// </summary>
    [DataContract]
    public class CreatePaymentPetitionResponse: SbpNspkBaseResponse<CreatePaymentPetitionData>
    {
        public CreatePaymentPetitionResponse(string code, string message, CreatePaymentPetitionData? data)
            : base(code, message, data)
        {
        }
    }
}