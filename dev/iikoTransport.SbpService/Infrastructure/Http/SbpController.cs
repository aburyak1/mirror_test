using System;
using System.Threading.Tasks;
using iikoTransport.SbpService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iikoTransport.SbpService.Infrastructure.Http
{
    /// <summary>
    /// Операции с Функциональными ссылками СБП для Агентов ТСП QrcData
    /// </summary>
    public class SbpController : ControllerBase
    {
        private readonly ISbpService sbpService;

        public SbpController(ISbpService sbpService)
        {
            this.sbpService = sbpService ?? throw new ArgumentNullException(nameof(sbpService));
        }
        
        /// <summary>
        /// Тестовый метод для проверки работоспособности.
        /// </summary>
        [HttpGet]
        [Route("test1")]
        public async Task<string> TestRun1()
        {
            return await sbpService.TestRun1();
        }
        
        /// <summary>
        /// Тестовый метод для проверки работоспособности.
        /// </summary>
        [HttpGet]
        [Route("test2")]
        public async Task<string> TestRun2(Guid? tgId)
        {
            return await sbpService.TestRun2(tgId);
        }
        
        /// <summary>
        /// Тестовый метод для проверки работоспособности.
        /// </summary>
        [HttpGet]
        [Route("test3")]
        public async Task<string> TestRun3(string? qrcId)
        {
            return await sbpService.TestRun3(qrcId);
        }
    }
}