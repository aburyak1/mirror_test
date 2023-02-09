using System;
using System.Runtime.Serialization;

namespace iikoTransport.SbpService.IikoWebIntegration.Contracts
{
    /// <summary>
    /// Sbp settings changes response for requested revision.
    /// </summary>
    [DataContract]
    public class GetSbpSettingsChangesResponse
    {
        public GetSbpSettingsChangesResponse(SbpSettingsChanges data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));;
        }

        /// <summary>
        /// Sbp settings changes
        /// </summary>
        [DataMember(IsRequired = true)]
        public SbpSettingsChanges Data { get; }
    }
}