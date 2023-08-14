using System;
using System.Linq;
using System.Threading.Tasks;
using iikoTransport.SbpService.Contracts.FrontPlugin.FromFront;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.SbpService.Services.SbpNspk;
using iikoTransport.Service;
using iikoTransport.Service.Filters;
using Microsoft.AspNetCore.Mvc;
using PublicContracts = iikoTransport.SbpService.Contracts.PublicApi;

namespace iikoTransport.SbpService.Infrastructure.Http.Internal
{
    /// <summary>
    /// Контроллер для тестовых прогонов перечисленных методов в целях сертификации.
    /// todo: Удалить после закрытия задачи TRN-2150, прохождения сертификации и деплоя версии для боевого контура. 
    /// </summary>
    [ServiceFilter(typeof(InternalApiFilter))]
    public class TestSbpController : ControllerBase
    {
        private readonly IFrontPluginsSbpService frontPluginsSbpService;
        private readonly IPublicApiSbpService publicApiSbpService;
        private readonly SbpNspkClient sbpClient;
        private const string TestStartUri = "https://agent.sbp-cert3.cbrpay.ru/test/start/";
        private const string TestsStatusUri = "https://agent.sbp-cert3.cbrpay.ru/tests/status/agent";

        public TestSbpController(IFrontPluginsSbpService frontPluginsSbpService, IPublicApiSbpService publicApiSbpService, SbpNspkClient sbpClient)
        {
            this.frontPluginsSbpService = frontPluginsSbpService ?? throw new ArgumentNullException(nameof(frontPluginsSbpService));
            this.publicApiSbpService = publicApiSbpService ?? throw new ArgumentNullException(nameof(publicApiSbpService));
            this.sbpClient = sbpClient ?? throw new ArgumentNullException(nameof(sbpClient));
        }
        
        [HttpGet]
        public async Task<string> TestsStatus()
        {
            return await sbpClient.CallSpbNspkTestMethod(TestsStatusUri);
        }
        
        [HttpGet]
        public async Task<string> TestStart(string testNumber, string? qrcId = null)
        {
            var uriDetails = TestStartUri + testNumber;
            if (!string.IsNullOrWhiteSpace(qrcId))
            {
                uriDetails += $"&userQR={qrcId}";
            }
            return await sbpClient.CallSpbNspkTestMethod(uriDetails);
        }
        
        [HttpPost]
        public async Task<PaymentLinkPayloadResponse> QrcData01([FromBody] CreateReusablePaymentLinkRequest request)
        {
            return await frontPluginsSbpService.CreateReusablePaymentLink(HttpContext.CreateCall(request));
        }
        
        [HttpPost]
        public async Task<PaymentLinkPayloadResponse> QrcData02([FromBody] CreateOneTimePaymentLinkRequest request)
        {
            return await frontPluginsSbpService.CreateOneTimePaymentLink(HttpContext.CreateCall(request));
        }

        [HttpPost]
        public async Task<PaymentLinkPayloadResponse> QrcPayload([FromBody] GetQrcPayloadRequest request)
        {
            return await frontPluginsSbpService.GetQrcPayload(HttpContext.CreateCall(request));
        }
        
        [HttpPost]
        public async Task<PaymentLinkPayloadResponse> CashRegisterQrc()
        {
            var reservationRequest = new CreateQrcIdReservationRequest(1);
            var reservationResponse = await frontPluginsSbpService.CreateQrcIdReservation(HttpContext.CreateCall(reservationRequest));
            if (reservationResponse.Data == null)
            {
                throw new NullReferenceException(reservationResponse.Message);
            }
            var createRequest = new CreateCashRegisterQrRequest(reservationResponse.Data.QrcIds.First());
            return await frontPluginsSbpService.СreateCashRegisterQr(HttpContext.CreateCall(createRequest));
        }
        
        [HttpPost]
        public async Task<ActivateCashRegisterQrResponse> ActivateCashRegisterQrc([FromBody] ActivateCashRegisterQrRequest request)
        {
            return await frontPluginsSbpService.ActivateCashRegisterQr(HttpContext.CreateCall(request));
        }
        
        [HttpPost]
        public async Task<DeactivateCashRegisterQrResponse> DeactivateCashRegisterQrc([FromBody] DeactivateCashRegisterQrRequest request)
        {
            return await frontPluginsSbpService.DeactivateCashRegisterQr(HttpContext.CreateCall(request));
        }
        
        [HttpPost]
        public async Task<PublicContracts.GetMerchantDataResponse> MerchantInfo([FromBody] PublicContracts.GetMerchantDataRequest request)
        {
            return await publicApiSbpService.GetMerchantData(HttpContext.CreateCall(request));
        }
        
        [HttpPost]
        public async Task<CreatedRefundResponse> PostRefund([FromBody] CreateRefundRequest request)
        {
            return await frontPluginsSbpService.CreateRefundRequest(HttpContext.CreateCall(request));
        }
        
        [HttpPost]
        public async Task<GetRefundStatusResponse> GetRefund([FromBody] GetRefundStatusRequest request)
        {
            return await frontPluginsSbpService.GetRefundStatus(HttpContext.CreateCall(request));
        }
        
        [HttpPost]
        public async Task<PublicContracts.SearchMerchantDataResponse> MerchantSearch([FromBody] PublicContracts.SearchMerchantDataRequest request)
        {
            return await publicApiSbpService.SearchMerchantData(HttpContext.CreateCall(request));
        }
        
        [HttpPost]
        public async Task<GetStatusQrcOperationsResponse> QrcStatus([FromBody] GetStatusQrcOperationsRequest request)
        {
            return await frontPluginsSbpService.GetStatusQrcOperations(HttpContext.CreateCall(request));
        }
    }
}