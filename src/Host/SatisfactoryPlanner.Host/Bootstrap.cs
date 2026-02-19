using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SatisfactoryPlanner.API.Configuration.Modules;
using SatisfactoryPlanner.BuildingBlocks.Common.EnvironmentInfo;
using SatisfactoryPlanner.BuildingBlocks.Common.Options;

namespace SatisfactoryPlanner.Host
{
    public class Bootstrap(IStartupContext startupContext, ILoggerFactory loggerFactory)
    {
        private readonly ILogger<Bootstrap> _logger = loggerFactory.CreateLogger<Bootstrap>();

        public void Start()
        {
            _logger.LogInformation("Starting {AppName} - {ProcessPath} - Version {Version}",
                BuildInfo.AppName,
                Environment.ProcessPath,
                BuildInfo.Version);

            var builder = CreateConsoleHostBuilder();
            builder.Build().Run();
        }

        private IHostBuilder CreateConsoleHostBuilder()
        {
            var config = ConfigurationLoader.Load(startupContext, loggerFactory);

            return new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule<ApiModule>())
                .ConfigureServices(services =>
                {
                    services
                        .AddOptions<ServerOptions>()
                        .Bind(config.Root.GetSection(ServerOptions.ConfigurationSectionName))
                        .ValidateDataAnnotations()
                        .ValidateOnStart();
                })
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseConfiguration(config.Root);

                    const string scheme = "http";
                    var bindAddress = config.ServerOptions.BindAddress;
                    var port = config.ServerOptions.Port;
                    var url = $"{scheme}://{bindAddress}:{port}";
                    builder.UseUrls(url);

                    builder.UseKestrel();
                    builder.UseStartup<Startup>();
                });
        }
    }
}
