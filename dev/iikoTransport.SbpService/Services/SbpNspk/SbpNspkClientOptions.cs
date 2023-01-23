using System;
using iikoTransport.ServiceClient;

namespace iikoTransport.SbpService.Services
{
    /// <summary>
    /// Параметры клиента для вызова методов api СБП.
    /// </summary>
    /// <seealso cref="SbpNspkClient"/>
    public class SbpNspkClientOptions : IClientOptions
    {
        public SbpNspkClientOptions(string baseUri, TimeSpan timeout)
        {
            BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
            Timeout = timeout;
        }

        public string BaseUri { get; }

        public TimeSpan Timeout { get; }
    }
}