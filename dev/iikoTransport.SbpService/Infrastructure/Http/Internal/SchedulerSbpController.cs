using System;
using System.Threading.Tasks;
using iikoTransport.SbpService.Services.Interfaces;
using iikoTransport.Service;
using iikoTransport.Service.Filters;
using Microsoft.AspNetCore.Mvc;

namespace iikoTransport.SbpService.Infrastructure.Http.Internal
{
    /// <summary>
    /// Контроллер для методов SchedulerService для транспорта.
    /// </summary>
    [ServiceFilter(typeof(InternalApiFilter))]
    public class SchedulerSbpController : ControllerBase
    {
        private readonly ISchedulerSbpService schedulerSbpService;

        public SchedulerSbpController(ISchedulerSbpService schedulerEmployeesService)
        {
            this.schedulerSbpService = schedulerEmployeesService ?? throw new ArgumentNullException(nameof(schedulerEmployeesService));
        }
        
        /// <inheritdoc cref="ISchedulerSbpService.SyncSbpSettingsWithIikoWeb"/>
        [HttpPost]
        public async Task SyncSettingsWithIikoWeb()
        {
            await schedulerSbpService.SyncSbpSettingsWithIikoWeb(HttpContext.GetCallContext());
        }
    }
}