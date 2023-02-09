using System;
using System.Security.Cryptography.X509Certificates;
using iikoTransport.SbpService.Infrastructure.Settings;

namespace iikoTransport.SbpService.Services.SbpNspk
{
    /// <summary>
    /// Class for building SbpClient certificate. 
    /// </summary>
    public class SbpClientCertBuilder : ISbpClientCertBuilder
    {
        private readonly string certPath;
        private readonly string password;

        public SbpClientCertBuilder(IServicesSettings serviceSettings)
        {
            if (serviceSettings == null)
                throw new ArgumentNullException(nameof(serviceSettings));
            certPath = serviceSettings.SbpNspkCertPath;
            password = serviceSettings.SbpNspkCertPass;
        }

        /// <inheritdoc />
        public X509Certificate2 Build()
        {
            var cert = new X509Certificate2(certPath, password);
            return cert;
        }
    }
}