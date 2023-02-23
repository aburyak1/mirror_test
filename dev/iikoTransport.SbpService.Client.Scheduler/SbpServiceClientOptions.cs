using System;
using iikoTransport.ServiceClient;

namespace iikoTransport.SbpService.Client.Scheduler
{
    public class SbpServiceClientOptions : IClientOptions
    {
        public SbpServiceClientOptions(string baseUri)
        {
            if (string.IsNullOrWhiteSpace(baseUri)) throw new ArgumentNullException(nameof(baseUri));
            BaseUri = baseUri;
        }

        public string BaseUri { get; }
    }
}