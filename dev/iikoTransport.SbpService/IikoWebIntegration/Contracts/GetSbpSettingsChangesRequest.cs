using System.Runtime.Serialization;

namespace iikoTransport.SbpService.IikoWebIntegration.Contracts
{
    /// <summary>
    /// Request for SBP settings changes.
    /// </summary>
    [DataContract]
    public class GetSbpSettingsChangesRequest
    {
        public GetSbpSettingsChangesRequest(long startRevision)
        {
            StartRevision = startRevision;
        }

        /// <summary>
        /// Start revision from which return changes.
        /// </summary>
        [DataMember(IsRequired = true)]
        public long StartRevision { get; }
    }
}