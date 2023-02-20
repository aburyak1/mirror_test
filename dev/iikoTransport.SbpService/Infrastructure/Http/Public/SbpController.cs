using System;
using System.Threading.Tasks;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.SbpService.Services.SbpNspk.Contracts.Events;
using iikoTransport.Service;
using iikoTransport.Service.Filters;
using Microsoft.AspNetCore.Mvc;

namespace iikoTransport.SbpService.Infrastructure.Http.Public
{
    /// <summary>
    /// Уведомления для Агентов ТСП. 
    /// </summary>
    [ServiceFilter(typeof(InternalApiFilter))]
    public class SbpController : ControllerBase
    {
        private readonly ISbpEventsService eventsService;

        public SbpController(ISbpEventsService eventsService)
        {
            this.eventsService = eventsService ?? throw new ArgumentNullException(nameof(eventsService));
        }
        
        /// <summary>
        /// Уведомление для Агента ТСП о финальном статусе Операции СБП C2B (v2).
        /// </summary>
        [HttpPost]
        [Route("payment/v2/qrc/notification")]
        public async Task SendFinalStatusAckV2([FromBody] SendFinalStatusAckV2Request request)
        {
            await eventsService.SendFinalStatusAckV2(HttpContext.CreateCall(request));
        }
        
        /// <summary>
        /// Уведомление для Агента ТСП о финальном статусе Операции СБП B2C.
        /// </summary>
        [HttpPost]
        [Route("payment/v1/b2c-refund/notification")]
        public async Task SendFinalStatusB2CAck([FromBody] SendFinalStatusB2CAckRequest request)
        {
            await eventsService.SendFinalStatusB2CAck(HttpContext.CreateCall(request));
        }
        
        /// <summary>
        /// Уведомление для Агента ТСП о результате обработки запроса на возврат.
        /// </summary>
        [HttpPost]
        [Route("payment/v1/refund/notification")]
        public async Task RefundResolution([FromBody] RefundResolutionRequest request)
        {
            await eventsService.RefundResolution(HttpContext.CreateCall(request));
        }
    }
}