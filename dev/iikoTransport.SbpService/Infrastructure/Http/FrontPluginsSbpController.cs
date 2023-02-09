using System;
using System.Threading.Tasks;
using iikoTransport.SbpService.Contracts.FrontPlugin;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.Service;
using Microsoft.AspNetCore.Mvc;

namespace iikoTransport.SbpService.Infrastructure.Http
{
    /// <summary>
    /// Controller for front plugins called.
    /// </summary>
    public class FrontPluginsSbpController : ControllerBase
    {
        private readonly IFrontPluginsSbpService frontPluginsSbpService;

        public FrontPluginsSbpController(IFrontPluginsSbpService frontPluginsSbpService)
        {
            this.frontPluginsSbpService = frontPluginsSbpService ?? throw new ArgumentNullException(nameof(frontPluginsSbpService));
        }
        
        /// <summary>
        /// Регистрация одноразовой Функциональной ссылки СБП для B2B.
        /// </summary>
        [HttpPost]
        public async Task<PaymentLinkPayloadResponse> CreateOneTimePaymentLink([FromBody] CreateOneTimePaymentLinkRequest request)
        {
            return await frontPluginsSbpService.CreateOneTimePaymentLink(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Регистрация многоразовой Функциональной ссылки СБП для B2B.
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
        /// Запрос статуса Операций СБП по идентификаторам QR. 
        /// </summary>
        [HttpPost]
        public async Task<GetStatusQrcOperationsResponse> GetStatusQrcOperations([FromBody] GetStatusQRCOperationsRequest request)
        {
            return await frontPluginsSbpService.GetStatusQrcOperations(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Запрос Агента ТСП на возврат по Операции СБП.
        /// </summary>
        [HttpPost]
        public async Task<CreatePaymentPetitionResponse> CreatePaymentPetition([FromBody] CreatePaymentPetitionRequest request)
        {
            return await frontPluginsSbpService.CreatePaymentPetition(HttpContext.CreateCall(request));
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