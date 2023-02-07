using System.Runtime.Serialization;

namespace iikoTransport.SbpService.Contracts.FrontPlugin
{
    /// <summary>
    /// Ответ на деактивацию Кассовой ссылки СБП для выполнения платежа.
    /// </summary>
    [DataContract]
    public class GetRefundStatusResponse: SbpNspkBaseResponse<GetRefundStatusData>
    {
        public GetRefundStatusResponse(string code, string message, GetRefundStatusData? data)
            : base(code, message, data)
        {
        }
    }
}