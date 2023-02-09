using System;
using iikoTransport.SbpService.Storage;

namespace iikoTransport.SbpService.Infrastructure.Settings
{
    /// <summary>
    /// Application settings.
    /// </summary>
    public class AppSettings : IDbSettings, IServicesSettings
    {
        public string IikoWebServiceAddress { get; set; } = null!;

        public string? IikoWebProxyAddress { get; set; }
        
        public string? IikoWebProxyUser { get; set; }
        
        public string? IikoWebProxyPassword { get; set; }
        
        /// <inheritdoc />
        public TimeSpan IikoWebCallTimeout { get; set; }
        
        public string ConnectionString { get; set; } = null!;
        
        public TimeSpan DefaultCallTimeout { get; set; }
        
        public int DefaultRetryCount { get; set; }
        
        public string SbpNspkUriFormat { get; set; } = null!;
        
        public TimeSpan SbpNspkTimeout { get; set; }
        
        public string SbpNspkAgentId { get; set; } = null!;
        
        public string SbpNspkCertPath { get; set; } = null!;
        
        public string SbpNspkCertPass { get; set; } = null!;
    }
}