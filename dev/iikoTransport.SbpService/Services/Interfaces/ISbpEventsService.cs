using System.Threading.Tasks;
using iikoTransport.SbpService.Services.SbpNspk.Contracts.Events;
using iikoTransport.Service;

namespace iikoTransport.SbpService.Services.Interfaces
{
    public interface ISbpEventsService
    {
        /// <summary>
        /// Уведомление для Агента ТСП о финальном статусе Операции СБП C2B (v2).
        /// </summary>
        Task SendFinalStatusAckV2(Call<SendFinalStatusAckV2Request> call);
        
        /// <summary>
        /// Уведомление для Агента ТСП о финальном статусе Операции СБП B2C.
        /// </summary>
        Task SendFinalStatusB2CAck(Call<SendFinalStatusB2CAckRequest> call);
        
        /// <summary>
        /// Уведомление для Агента ТСП о результате обработки запроса на возврат. 
        /// </summary>
        Task RefundResolution(Call<RefundResolutionRequest> call);
    }
}