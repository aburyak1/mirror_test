using System;
using System.Threading.Tasks;
using iikoTransport.Postgres;
using iikoTransport.Postgres.JobLocks;
using iikoTransport.Postgres.Synchronization;
using iikoTransport.SbpService.Infrastructure.DI;
using iikoTransport.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace iikoTransport.SbpService
{
    public class Startup : WebApiHostConfiguration
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomBindings(Configuration);
            services.UseJobLocksPostgres();
            services.UseSynchronizationPostgres();
            base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IHostApplicationLifetime appLifetime)
        {
            base.Configure(app, appLifetime);
            ConfigureAsync(app.ApplicationServices)
                .GetAwaiter()
                .GetResult();
			// ITS SYRVE FROM GITHUB
        }

        private static async Task ConfigureAsync(IServiceProvider serviceProvider)
        {
            await serviceProvider.GetRequiredService<IDbContextFactory>()
                .CreateStorageInitializer()
                .UpgradeStorage();
            await serviceProvider.InitJobLocksPostgresAsync();
            await serviceProvider.InitSynchronizationPostgresAsync();
        }
    }
}