using System.Threading.Tasks;

namespace iikoTransport.SbpService.Services.Interfaces
{
    public interface ISbpService
    {
        /// <summary>
        /// Тестовый запрос к СБП.
        /// </summary>
        Task<string> TestRun();
    }
}