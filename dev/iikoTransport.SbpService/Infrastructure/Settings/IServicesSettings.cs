using System;

namespace iikoTransport.SbpService.Infrastructure.Settings
{
    /// <summary>
    /// Settings for call of other services.
    /// </summary>
    public interface IServicesSettings
    {
        /// <summary>
        /// IikoWeb api address.
        /// </summary>
        string IikoWebServiceAddress { get; }
        
        /// <summary>
        /// IikoWeb proxy address.
        /// </summary>
        string? IikoWebProxyAddress { get; }
        
        /// <summary>
        /// User name for iikoWeb proxy.
        /// </summary>
        string? IikoWebProxyUser { get; }
        
        /// <summary>
        /// Password for iikoWeb proxy.
        /// </summary>
        string? IikoWebProxyPassword { get; }
        
        /// <summary>
        /// Timeout to iikoWeb http calls.
        /// </summary>
        TimeSpan IikoWebCallTimeout { get; }
        
        /// <summary>
        /// Default timeout for http-calls. 
        /// </summary>
        TimeSpan DefaultCallTimeout { get; }

        /// <summary>
        /// Default retry count for http-calls.
        /// </summary>
        int DefaultRetryCount { get; }
        
        /// <summary>
        /// Uri для обращения к api СБП.
        /// </summary>
        string SbpNspkUriFormat { get; }

        /// <summary>
        /// Таймаут для запросов к api СБП.
        /// </summary>
        TimeSpan SbpNspkTimeout { get; }
        
        /// <summary>
        /// Идентификатор Агента ТСП.
        /// </summary>
        string SbpNspkAgentId { get; }
        
        /// <summary>
        /// Путь к файлу сертификата Агента ТСП.
        /// </summary>
        string SbpNspkCertPath { get; }
        
        /// <summary>
        /// Пароль доступа к данным сертификата Агента ТСП.
        /// </summary>
        string SbpNspkCertPass { get; }
    }
}