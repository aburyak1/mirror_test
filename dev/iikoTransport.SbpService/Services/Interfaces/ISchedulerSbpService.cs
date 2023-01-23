using System.Threading.Tasks;
using iikoTransport.Service;

namespace iikoTransport.SbpService.Services.Interfaces
{
    /// <summary>
    /// Сервис для обработки запросов от SchedulerService.
    /// </summary>
    public interface ISchedulerSbpService
    {
        /// <summary>
        /// Synchronize sbp settings with iikoWeb.
        /// </summary>
        Task SyncSbpSettingsWithIikoWeb(CallContext callContext);
    }
}