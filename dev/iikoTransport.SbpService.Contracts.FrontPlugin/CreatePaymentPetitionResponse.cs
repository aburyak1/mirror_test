using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Ответ на получение массива идентификаторов многоразовых ссылок СБП для последующей регистрации ссылки с заданным идентификатором.
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