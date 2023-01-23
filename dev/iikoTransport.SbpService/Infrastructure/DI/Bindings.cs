using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using iikoTransport.EmployeesService.IikoWebIntegration;
using iikoTransport.Logging;
using iikoTransport.Logging.Events;
using iikoTransport.Logging.Metrics;
using iikoTransport.Logging.Serilog;
using iikoTransport.Postgres;
using iikoTransport.SbpService.IikoWebIntegration;
using iikoTransport.SbpService.Infrastructure.Settings;
using iikoTransport.SbpService.Services;
using iikoTransport.SbpService.Services.Interfaces;
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
                return new SbpNspkClientOptions(servicesSettings.SbpNspkUriFormat, servicesSettings.SbpNspkTimeout);
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
            
            services.AddSingleton<ISbpService, Services.SbpService>();
            services.AddScoped<ISchedulerSbpService, SchedulerSbpService>();
            services.AddScoped<IIikoWebSyncManager, IikoWebSyncManager>();

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
            services.AddHttpClient<SbpNspkClient>();

            services.AddDocumentation(new[]
            {
                new ApiSpecification("internal", "iikoTransport.SbpService internal API")
            });
        }
    }
}