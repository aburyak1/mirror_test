using System.Threading.Tasks;
using iikoTransport.SbpService.Storage.Contracts.Entities;

namespace iikoTransport.SbpService.Storage.Contracts
{
    public interface ISbpSettingsStorage
    {
        /// <summary>
        /// Save or insert SBP system.
        /// </summary>
        Task Upsert(params SbpSetting[] sbpSettings);
    }
}