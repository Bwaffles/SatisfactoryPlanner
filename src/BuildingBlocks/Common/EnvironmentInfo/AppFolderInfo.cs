using Microsoft.Extensions.Logging;

namespace SatisfactoryPlanner.BuildingBlocks.Common.EnvironmentInfo
{
    /// <summary>
    /// Holds information about the application folders.
    /// </summary>
    public interface IAppFolderInfo
    {
        /// <summary>
        /// The path to the folder for storing all the application data.
        /// </summary>
        string AppDataFolder { get; }
    }

    /// <inheritdoc cref="IAppFolderInfo"/>
    public class AppFolderInfo : IAppFolderInfo
    {
        private readonly Environment.SpecialFolder _dataSpecialFolder = Environment.SpecialFolder.CommonApplicationData;

        /// <inheritdoc/>
        public string AppDataFolder { get; }

        public AppFolderInfo(IStartupContext startupContext, ILogger logger)
        {
            if (!string.IsNullOrWhiteSpace(startupContext.AppData))
            {
                AppDataFolder = startupContext.AppData;
                logger.LogInformation("Data directory is being overridden to {AppDataFolder}", AppDataFolder);
            }
            else
            {
                AppDataFolder = Path.Combine(Environment.GetFolderPath(_dataSpecialFolder, Environment.SpecialFolderOption.DoNotVerify), "SatisfactoryPlanner");
            }
        }
    }
}
