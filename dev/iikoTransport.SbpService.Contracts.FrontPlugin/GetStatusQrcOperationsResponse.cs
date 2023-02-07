using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Ответ на запрос статуса Операций СБП по идентификаторам QR
    /// </summary>
    [DataContract]
    public class GetStatusQrcOperationsResponse: SbpNspkBaseResponse<GetStatusQrcOperationsData[]>
    {
        public GetStatusQrcOperationsResponse(string code, string message, GetStatusQrcOperationsData[]? data)
            : base(code, message, data)
        {
        }
    }
}