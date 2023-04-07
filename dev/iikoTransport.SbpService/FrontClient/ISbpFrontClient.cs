using System;
using System.Threading.Tasks;
using iikoTransport.ServiceClient;

namespace iikoTransport.SbpService.FrontClient
{
    /// <summary>
    /// Интерфейс для взаимодействия с фронтом.
    /// </summary>
    public interface ISbpFrontClient
    {
        /// <summary>
        /// Call iikoFront plugin method "methodUri".
        /// Plugin is identified by it's "pluginModuleId".
        /// </summary>
        /// <param name="methodUri">Method uri of plugin api.</param>
        /// <param name="bodyJson">Body to pass to plugin method.</param>
        /// <param name="terminalGroupUocId">Terminal Group ID in UOC.</param>
        /// <param name="terminalId">Terminal ID in RMS.</param>
        /// <param name="pluginModuleId">Plugin licence id.</param>
        /// <param name="callSettings">http call settings to call TransportService.</param>
        Task CallFrontPluginMethod(string methodUri, object bodyJson, Guid terminalGroupUocId, Guid? terminalId, int pluginModuleId, MethodCallSettings callSettings);
    }
}