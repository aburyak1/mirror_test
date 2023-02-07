using System;
using iikoTransport.ServiceClient;

namespace iikoTransport.SbpService.Services.SbpNspk
{
    /// <summary>
    /// Параметры клиента для вызова методов api СБП.
    /// </summary>
    /// <seealso cref="SbpNspkClient"/>
    public class SbpNspkClientOptions : IClientOptions
    {
        public SbpNspkClientOptions(string baseUri, TimeSpan timeout, string agentId)
        {
            BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
            Timeout = timeout;
            AgentId = agentId ?? throw new ArgumentNullException(nameof(agentId));
        }

        public string BaseUri { get; }

        public TimeSpan Timeout { get; }

        public string AgentId { get; }
    }
}