using System;
using System.Threading.Tasks;
using iikoTransport.SbpService.Contracts.FrontPlugin.FromFront;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.Service;
using iikoTransport.Service.Filters;
using Microsoft.AspNetCore.Mvc;

namespace iikoTransport.SbpService.Infrastructure.Http.Internal
{
    /// <summary>
    /// Controller for front plugins called.
    /// </summary>
    [ServiceFilter(typeof(InternalApiFilter))]
    public class FrontPluginsSbpController : ControllerBase
    {
        private readonly IFrontPluginsSbpService frontPluginsSbpService;

        public FrontPluginsSbpController(IFrontPluginsSbpService frontPluginsSbpService)
        {
            this.frontPluginsSbpService = frontPluginsSbpService ?? throw new ArgumentNullException(nameof(frontPluginsSbpService));
        }
        
        /// <summary>
        /// Регистрация одноразовой Функциональной ссылки СБП.
        /// </summary>
        [HttpPost]
        public async Task<PaymentLinkPayloadResponse> CreateOneTimePaymentLink([FromBody] CreateOneTimePaymentLinkRequest request)
        {
            return await frontPluginsSbpService.CreateOneTimePaymentLink(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Регистрация многоразовой Функциональной ссылки СБП.
        /// </summary>
        [HttpPost]
        public async Task<PaymentLinkPayloadResponse> CreateReusablePaymentLink([FromBody] CreateReusablePaymentLinkRequest request)
        {
            return await frontPluginsSbpService.CreateReusablePaymentLink(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Запрос содержимого для ранее зарегистрированной Функциональной ссылки СБП.
        /// </summary>
        [HttpPost]
        public async Task<PaymentLinkPayloadResponse> GetQrcPayload([FromBody] GetQrcPayloadRequest request)
        {
            return await frontPluginsSbpService.GetQrcPayload(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Запрос статуса Операций СБП по идентификаторам QR Dynamic. 
        /// </summary>
        [HttpPost]
        public async Task<GetStatusQrcOperationsResponse> GetStatusQrcOperations([FromBody] GetStatusQrcOperationsRequest request)
        {
            return await frontPluginsSbpService.GetStatusQrcOperations(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Получение идентификаторов для многоразовых ссылок СБП.
        /// </summary>
        [HttpPost]
        public async Task<CreateQrcIdReservationResponse> CreateQrcIdReservation([FromBody] CreateQrcIdReservationRequest request)
        {
            return await frontPluginsSbpService.CreateQrcIdReservation(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Регистрация Кассовой ссылки СБП.
        /// </summary>
        [HttpPost]
        public async Task<PaymentLinkPayloadResponse> СreateCashRegisterQr([FromBody] CreateCashRegisterQrRequest request)
        {
            return await frontPluginsSbpService.СreateCashRegisterQr(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Активация Кассовой ссылки СБП.
        /// </summary>
        [HttpPost]
        public async Task<ActivateCashRegisterQrResponse> ActivateCashRegisterQr([FromBody] ActivateCashRegisterQrRequest request)
        {
            return await frontPluginsSbpService.ActivateCashRegisterQr(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Деактивация Кассовой ссылки СБП.
        /// </summary>
        [HttpPost]
        public async Task<DeactivateCashRegisterQrResponse> DeactivateCashRegisterQr([FromBody] DeactivateCashRegisterQrRequest request)
        {
            return await frontPluginsSbpService.DeactivateCashRegisterQr(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Запрос статуса Кассовой ссылки СБП.
        /// </summary>
        [HttpPost]
        public async Task<GetCashRegQrStatusResponse> GetCashRegQrStatus([FromBody] GetCashRegQrStatusRequest request)
        {
            return await frontPluginsSbpService.GetCashRegQrStatus(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Статус Операции по Кассовой ссылке СБП.
        /// </summary>
        [HttpPost]
        public async Task<GetStatusCashRegQrOperationResponse> GetStatusCashRegQrOperation([FromBody] GetStatusCashRegQrOperationRequest request)
        {
            return await frontPluginsSbpService.GetStatusCashRegQrOperation(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Запрос Агента ТСП на возврат по Операции СБП.
        /// </summary>
        [HttpPost]
        public async Task<CreatedRefundResponse> CreateRefundRequest([FromBody] CreateRefundRequest request)
        {
            return await frontPluginsSbpService.CreateRefundRequest(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Получение идентификатора ОПКЦ СБП запроса на возврат.
        /// </summary>
        [HttpPost]
        public async Task<CreatedRefundResponse> GetRefundIdRequest([FromBody] GetRefundIdRequest request)
        {
            return await frontPluginsSbpService.GetRefundIdRequest(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Статус запроса на возврат средств для Агента ТСП.
        /// </summary>
        [HttpPost]
        public async Task<GetRefundStatusResponse> GetRefundStatus([FromBody] GetRefundStatusRequest request)
        {
            return await frontPluginsSbpService.GetRefundStatus(HttpContext.CreateCall(request));
        }
    }
}