namespace SatisfactoryPlanner.BuildingBlocks.Common.EnvironmentInfo
{
    /// <summary>
    /// The context around the application startup. Allows for generic configuration of the startup
    /// that can be used before all services have been set up.
    /// </summary>
    public interface IStartupContext
    {
        /// <summary>
        /// The path to the folder for the application data, or null to use the default.
        /// </summary>
        string? AppData { get; set; }
    }

    /// <summary>
    /// <inheritdoc cref="IStartupContext"/>
    /// </summary>
    public class StartupContext : IStartupContext
    {
        /// <inheritdoc/>
        public string? AppData { get; set; }
    }
}
