namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration
{
    public class WorldsConfiguration
    {
        /// <summary>
        /// Determines how often the internal processing jobs run.
        /// <para>Default is every 2 seconds.</para>
        /// </summary>
        public TimeSpan InternalProcessingExecutionInterval { get; set; } = TimeSpan.FromSeconds(2);
    }
}