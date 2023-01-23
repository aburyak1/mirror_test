using System.Threading.Tasks;
using iikoTransport.Service;

namespace iikoTransport.SbpService.IikoWebIntegration
{
    /// <summary>
    /// IikoWeb synchronization manager interface.
    /// </summary>
    public interface IIikoWebSyncManager
    {
        /// <summary>
        /// Synchronize sbp settings.
        /// </summary>
        Task SyncSbpSettings(CallContext callContext);
    }
}