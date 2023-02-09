using System;
using System.Threading;
using System.Threading.Tasks;
using iikoTransport.SbpService.Storage.Contracts.Entities;

namespace iikoTransport.SbpService.Storage.Contracts
{
    public interface ISbpSettingsStorage
    {
        /// <summary>
        /// Get sbp setting for terminalGroup.
        /// </summary>
        Task<SbpSetting> Get(Guid terminalGroupUocId, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Save or insert SBP system.
        /// </summary>
        Task Upsert(SbpSetting[] sbpSettings, CancellationToken cancellationToken = default);
    }
}