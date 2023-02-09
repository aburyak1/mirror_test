using System.Linq;
using WEB = iikoTransport.SbpService.IikoWebIntegration.Contracts;
using DB = iikoTransport.SbpService.Storage.Contracts.Entities;

namespace iikoTransport.SbpService.Converters
{
    /// <summary>
    /// Converts iikoWeb contracts to storage entites.
    /// </summary>
    public static class IikoWebToStorageConverters
    {
        public static DB.SbpSetting[] Convert(this WEB.NspkInfo[] source)
        {
            return source.Select(Convert).ToArray();
        }
        
        public static DB.SbpSetting Convert(this WEB.NspkInfo source)
        {
            return new DB.SbpSetting(source.Id, source.TerminalGroupUocId, source.MerchantId, source.Account, source.MemberId);
        }
    }
}