using System.Security.Cryptography.X509Certificates;

namespace iikoTransport.SbpService.Services.SbpNspk
{
    /// <summary>
    /// Interface for building SbpClient certificate. 
    /// </summary>
    public interface ISbpClientCertBuilder
    {
        /// <summary>
        /// Bulid X509Certificate.
        /// </summary>
        X509Certificate2 Build();
    }
}