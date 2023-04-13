using System;
using System.Threading.Tasks;
using iikoTransport.SbpService.Contracts.PublicApi;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.Service;
using iikoTransport.Service.Filters;
using Microsoft.AspNetCore.Mvc;

namespace iikoTransport.SbpService.Infrastructure.Http.Internal
{
    /// <summary>
    /// Controller for publicApi called.
    /// </summary>
    [ServiceFilter(typeof(InternalApiFilter))]
    public class PublicApiSbpController : ControllerBase
    {
        private readonly IPublicApiSbpService publicApiSbpService;

        public PublicApiSbpController(IPublicApiSbpService publicApiSbpService)
        {
            this.publicApiSbpService = publicApiSbpService ?? throw new ArgumentNullException(nameof(publicApiSbpService));
        }
        
        /// <summary>
        /// Регистрация одноразовой Функциональной ссылки СБП для B2B.
        /// </summary>
        [HttpPost]
        public async Task<PaymentLinkPayloadResponse> CreateOneTimePaymentLink([FromBody] CreateOneTimePaymentLinkRequest request)
        {
            return await publicApiSbpService.CreateOneTimePaymentLink(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Запрос содержимого для ранее зарегистрированной Функциональной ссылки СБП.
        /// </summary>
        [HttpPost]
        public async Task<PaymentLinkPayloadResponse> GetQrcPayload([FromBody] GetQrcPayloadRequest request)
        {
            return await publicApiSbpService.GetQrcPayload(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Запрос статуса Операций СБП по идентификаторам QR Dynamic. 
        /// </summary>
        [HttpPost]
        public async Task<GetStatusQrcOperationsResponse> GetStatusQrcOperations([FromBody] GetStatusQrcOperationsRequest request)
        {
            return await publicApiSbpService.GetStatusQrcOperations(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Запрос Агента ТСП на возврат по Операции СБП.
        /// </summary>
        [HttpPost]
        public async Task<CreatedRefundResponse> CreateRefundRequest([FromBody] CreateRefundRequest request)
        {
            return await publicApiSbpService.CreateRefundRequest(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Получение идентификатора ОПКЦ СБП запроса на возврат.
        /// </summary>
        [HttpPost]
        public async Task<CreatedRefundResponse> GetRefundIdRequest([FromBody] GetRefundIdRequest request)
        {
            return await publicApiSbpService.GetRefundIdRequest(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Статус запроса на возврат средств для Агента ТСП.
        /// </summary>
        [HttpPost]
        public async Task<GetRefundStatusResponse> GetRefundStatus([FromBody] GetRefundStatusRequest request)
        {
            return await publicApiSbpService.GetRefundStatus(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Изменение счета ЮЛ, ИП или самозанятого для зарегистрированной Кассовой ссылки СБП.
        /// </summary>
        [HttpPost]
        public async Task<SetNewAccountResponse> SetNewAccount([FromBody] SetNewAccountRequest request)
        {
            return await publicApiSbpService.SetNewAccount(HttpContext.CreateCall(request));
        }

        /// <summary>
        /// Получение списка ТСП, зарегистрированных для ЮЛ или ИП. 
        /// </summary>
        [HttpPost]
        public async Task<SearchMerchantDataResponse> SearchMerchantData([FromBody] SearchMerchantDataRequest request)
        {
            return await publicApiSbpService.SearchMerchantData(HttpContext.CreateCall(request));
        }
    }
}