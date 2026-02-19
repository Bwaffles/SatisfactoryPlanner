using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SatisfactoryPlanner.BuildingBlocks.Common.EnvironmentInfo;
using SatisfactoryPlanner.BuildingBlocks.Common.Extensions;
using SatisfactoryPlanner.BuildingBlocks.Common.Options;

namespace SatisfactoryPlanner.Host
{
    public sealed class ConfigurationLoader
    {
        public static LoadedConfiguration Load(IStartupContext startupContext, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<ConfigurationLoader>();
            logger.LogInformation("Loading Configuration...");

            var appFolder = new AppFolderInfo(startupContext, loggerFactory);
            var configPath = appFolder.GetConfigPath();

            var config = new ConfigurationBuilder()
                .AddJsonFile(configPath, optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            var serverOptions = new ServerOptions();
            LoadOptions(config, logger, ServerOptions.ConfigurationSectionName, ref serverOptions);

            return new LoadedConfiguration()
            {
                Root = config,
                ServerOptions = serverOptions
            };
        }

        private static void LoadOptions<TOptions>(IConfigurationRoot config, ILogger<ConfigurationLoader> logger, string sectionName, ref TOptions options)
        {
            var fullSectionName = $"SatisfactoryPlanner:{sectionName}";
            config.GetSection(fullSectionName).Bind(options);

            foreach (var property in typeof(TOptions).GetProperties())
            {
                var key = property.Name;
                var value = property.GetValue(options);
                logger.LogInformation("{fullSectionName}:{key}={value}", fullSectionName, key, value);
            }
        }

        public class LoadedConfiguration
        {
            public required IConfiguration Root;
            public required ServerOptions ServerOptions;
        }
    }
}
