using System;
using iikoTransport.ServiceClient;

namespace iikoTransport.SbpService.FrontClient
{
    /// <summary>
    /// Параметры для клиента взаимодействия с фронтом <see cref="ISbpFrontClient"/>.
    /// </summary>
    public class SbpFrontClientOptions : IClientOptions
    {
        public SbpFrontClientOptions(string baseUri, TimeSpan expiration)
        {
            BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
            Expiration = expiration;
        }

        public string BaseUri { get; }

        public TimeSpan Expiration { get; }
    }
}