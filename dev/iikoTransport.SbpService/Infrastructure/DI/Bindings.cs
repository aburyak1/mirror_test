using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Authentication;
using iikoTransport.DictionariesService.Client.Transport;
using iikoTransport.EmployeesService.IikoWebIntegration;
using iikoTransport.Logging;
using iikoTransport.Logging.Events;
using iikoTransport.Logging.Metrics;
using iikoTransport.Logging.Serilog;
using iikoTransport.Postgres;
using iikoTransport.SbpService.FrontClient;
using iikoTransport.SbpService.IikoWebIntegration;
using iikoTransport.SbpService.Infrastructure.Settings;
using iikoTransport.SbpService.Services;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.SbpService.Services.SbpNspk;
using iikoTransport.SbpService.Storage;
using iikoTransport.SbpService.Storage.Contracts;
using iikoTransport.Service;
using iikoTransport.Service.Documentation;
using iikoTransport.ServiceClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iikoTransport.SbpService.Infrastructure.DI
{
    public static class Bindings
    {
        public static void AddCustomBindings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ILoggerFactory>(provider =>
            {
                var metrics = provider.GetRequiredService<IMetrics>();
                return new SerilogLoggerFactory(configuration, metrics);
            });
            services.AddSingleton(provider =>
            {
                var logFactory = provider.GetRequiredService<ILoggerFactory>();
                return logFactory.GetLogger();
            });
            services.AddSingleton(provider =>
            {
                var logFactory = provider.GetRequiredService<ILoggerFactory>();
                return logFactory.GetStructuredLogger();
            });
            services.AddSingleton(new InstanceInfo(Guid.NewGuid(), "SbpService"));
            services.AddSingleton<IEventLoggerFactory>(provider =>
            {
                var structuredLog = provider.GetRequiredService<IStructuredLog>();
                var instanceInfo = provider.GetRequiredService<InstanceInfo>();
                return new EventLoggerFactory(structuredLog, instanceInfo);
            });
            services.AddSingleton(provider =>
            {
                var servicesSettings = provider.GetRequiredService<IServicesSettings>();
                return new SbpNspkClientOptions(servicesSettings.SbpNspkUriFormat, servicesSettings.SbpNspkTimeout, servicesSettings.SbpNspkAgentId);
            });

            var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            services.AddSingleton<IDbSettings>(appSettings);
            services.AddSingleton<IServicesSettings>(appSettings);

            services.AddSingleton<IDbContextFactory>(provider =>
            {
                var dbSettings = provider.GetRequiredService<IDbSettings>();
                var logger = provider.GetRequiredService<ILog>();
                var assembly = Assembly.GetExecutingAssembly();
                return new NpgsqlDbFactory(dbSettings.ConnectionString, logger, assembly);
            });
            services.AddSingleton<ISbpSettingsStorage, SbpSettingsStorage>();
            services.AddSingleton<IPaymentLinksStorage, PaymentLinksStorage>();
            services.AddSingleton<IRefundRequestsStorage, RefundRequestsStorage>();
            
            services.AddScoped<ISchedulerSbpService, SchedulerSbpService>();
            services.AddScoped<IIikoWebSyncManager, IikoWebSyncManager>();
            services.AddScoped<ISbpEventsService, SbpEventsService>();
            services.AddScoped<IFrontPluginsSbpService, FrontPluginsSbpService>();
            services.AddScoped<IPublicApiSbpService, PublicApiSbpService>();

            services.AddSingleton<IMethodCallSettingsFactory>(provider =>
            {
                var serviceSettings = provider.GetRequiredService<IServicesSettings>();
                return new MethodCallSettingsFactory(serviceSettings.DefaultCallTimeout, serviceSettings.DefaultRetryCount);
            });
            services.AddSingleton(provider =>
            {
                var servicesSettings = provider.GetRequiredService<IServicesSettings>();
                return new IikoWebClientOptions(servicesSettings.IikoWebServiceAddress);
            });
            services.AddSingleton(provider =>
            {
                var settings = provider.GetRequiredService<IServicesSettings>();
                return new SbpFrontClientOptions(settings.TransportServiceAddress, settings.DefaultCallTimeout);
            });
            services.AddSingleton(provider =>
            {
                var servicesSettings = provider.GetRequiredService<IServicesSettings>();
                return new DictionariesServiceClientOptions(servicesSettings.DictionariesServiceAddress);
            });
            
            services.AddHttpClient<IDictionariesServiceClient, DictionariesServiceClient>();
            services.AddHttpClient<ISbpFrontClient, SbpFrontClient>();
            
            var webClientBuilder = services.AddHttpClient<IIikoWebClient, IikoWebClient>();
            if (!string.IsNullOrWhiteSpace(appSettings.IikoWebProxyAddress))
                webClientBuilder.ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var proxyCredentials = new NetworkCredential(appSettings.IikoWebProxyUser, appSettings.IikoWebProxyPassword);
                    return new HttpClientHandler
                    {
                        Proxy = new WebProxy(appSettings.IikoWebProxyAddress, false, null, proxyCredentials)
                    };
                });

            services.AddSingleton<ISbpClientCertBuilder, SbpClientCertBuilder>();
            services.AddHttpClient<SbpNspkClient>().ConfigurePrimaryHttpMessageHandler(sp =>
            {
                var certBuilder = sp.GetRequiredService<ISbpClientCertBuilder>();
                var cert = certBuilder.Build();
                return new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    SslProtocols = SslProtocols.Tls12,
                    ClientCertificates = { cert },
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, certChain, policyErrors) => { return true; }
                };
            });

            services.AddDocumentation(new[]
            {
                new ApiSpecification("internal", "iikoTransport.SbpService internal API")
            });
        }
    }
}