using System.Threading.Tasks;
using iikoTransport.ServiceClient;

namespace iikoTransport.SbpService.Client.Scheduler
{
    public interface ISbpServiceClient
    {
        /// <summary>
        /// Synchronize sbp settings with iikoWeb.
        /// </summary>
        Task SyncSettingsWithIikoWeb(MethodCallSettings? callSettings);
    }
}