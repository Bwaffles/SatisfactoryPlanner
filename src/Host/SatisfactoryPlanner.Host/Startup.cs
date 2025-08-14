using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SatisfactoryPlanner.BuildingBlocks.Common.Instrumentation;
using Serilog;

namespace SatisfactoryPlanner.Host
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var hostLogger = SatisfactoryPlannerLogger.GetSerilogLogger(Module.Application);
            services.AddSerilog(hostLogger);
        }

        public void Configure(IApplicationBuilder app)
        {

        }
    }
}
