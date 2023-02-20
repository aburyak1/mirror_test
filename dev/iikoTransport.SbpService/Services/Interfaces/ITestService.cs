using System;
using System.Threading.Tasks;

namespace iikoTransport.SbpService.Services.Interfaces
{
    public interface ITestService
    {
        /// <summary>
        /// Тестовый запрос к СБП.
        /// </summary>
        Task<string> TestRun1();

        /// <summary>
        /// Тестовый запрос к СБП.
        /// </summary>
        Task<string> TestRun2(Guid? tgId = null);

        /// <summary>
        /// Тестовый запрос к СБП.
        /// </summary>
        Task<string> TestRun3(string? qrcId = null);
    }
}