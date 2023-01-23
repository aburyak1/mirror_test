using System.Runtime.Serialization;

namespace iikoTransport.SbpService.IikoWebIntegration.Contracts
{
    /// <summary>
    /// Sbp settings changes
    /// </summary>
    [DataContract]
    public class SbpSettingsChanges
    {
        public SbpSettingsChanges(long revision, NspkInfo[]? nspkSettings)
        {
            Revision = revision;
            NspkSettings = nspkSettings;
        }

        /// <summary>
        /// Last revision.
        /// </summary>
        [DataMember(IsRequired = true)]
        public long Revision { get; }
        
        /// <summary>
        /// Connected to requested terminal group Nspk's settings list. 
        /// </summary>
        [DataMember(IsRequired = false)]
        public NspkInfo[]? NspkSettings { get; }
    }
}