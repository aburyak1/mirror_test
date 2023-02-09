using System.Threading.Tasks;
using iikoTransport.SbpService.IikoWebIntegration.Contracts;
using iikoTransport.ServiceClient;

namespace iikoTransport.SbpService.IikoWebIntegration
{
    /// <summary>
    /// IikoWeb internal api client interface.
    /// </summary>
    public interface IIikoWebClient
    {
        /// <summary>
        /// Get sbp changes by specified revision.
        /// </summary>
        Task<SbpSettingsChanges> GetSbpSettingsChanges(GetSbpSettingsChangesRequest request, MethodCallSettings? callSettings);
    }
}