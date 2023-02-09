using System;
using iikoTransport.ServiceClient;

namespace iikoTransport.EmployeesService.IikoWebIntegration
{
    /// <summary>
    /// IikoWeb internal api client options.
    /// </summary>
    public class IikoWebClientOptions : IClientOptions
    {
        public IikoWebClientOptions(string baseUri)
        {
            BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }

        public string BaseUri { get; }
    }
}