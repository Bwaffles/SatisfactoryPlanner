using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SatisfactoryPlanner.BuildingBlocks.Common.EnvironmentInfo;
using System.Reflection;

namespace SatisfactoryPlanner.Host
{
    public class Bootstrap
    {
        public static void Start(IStartupContext startupContext, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<Bootstrap>();
            logger.LogInformation("Starting Satisfactory Planner - {ProcessPath} - Version {Version}",
                           Environment.ProcessPath,
                           Assembly.GetExecutingAssembly().GetName().Version);

            var builder = CreateConsoleHostBuilder(startupContext);
            builder.Build().Run();
        }

        public static IHostBuilder CreateConsoleHostBuilder(IStartupContext context)
        {
            return new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseKestrel();
                    builder.UseStartup<Startup>();
                });
        }
    }
}
