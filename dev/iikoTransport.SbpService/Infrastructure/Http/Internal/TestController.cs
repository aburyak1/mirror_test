using System;
using System.Threading.Tasks;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.Service.Filters;
using Microsoft.AspNetCore.Mvc;

namespace iikoTransport.SbpService.Infrastructure.Http.Internal
{
    /// <summary>
    /// Тестовые операции с Функциональными ссылками СБП для Агентов ТСП
    /// </summary>
    [ServiceFilter(typeof(InternalApiFilter))]
    public class TestController : ControllerBase
    {
        private readonly ITestService testService;

        public TestController(ITestService testService)
        {
            this.testService = testService ?? throw new ArgumentNullException(nameof(testService));
        }
        
        /// <summary>
        /// Тестовый метод для проверки работоспособности.
        /// </summary>
        [HttpGet]
        [Route("test1")]
        public async Task<string> TestRun1()
        {
            return await testService.TestCreateQrcIdReservation();
        }
        
        /// <summary>
        /// Тестовый метод для проверки работоспособности.
        /// </summary>
        [HttpGet]
        [Route("test2")]
        public async Task<string> TestRun2(Guid? tgId)
        {
            return await testService.TestCreateAndGetOneTimePaymentLinkPayloadForB2B(tgId);
        }
        
        /// <summary>
        /// Тестовый метод для проверки работоспособности.
        /// </summary>
        [HttpGet]
        [Route("test3")]
        public async Task<string> TestRun3(string? qrcId)
        {
            return await testService.TestGetQRCPayload(qrcId);
        }
    }
}