using System;
using iikoTransport.ServiceClient;

namespace iikoTransport.SbpService.Client.PublicApi
{
    public class SbpServiceClientOptions : IClientOptions
    {
        public SbpServiceClientOptions(string baseUri)
        {
            BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }

        public string BaseUri { get; }
    }
}