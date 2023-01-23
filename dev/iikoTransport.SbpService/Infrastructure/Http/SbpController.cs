using System;
using System.Threading.Tasks;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [Route("test")]
        public async Task<string> TestRun()
        {
            return await sbpService.TestRun();
        }
    }
}