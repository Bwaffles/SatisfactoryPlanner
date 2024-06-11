using System;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration
{
    public class ResourcesConfiguration
    {
        /// <summary>
        /// Determines how often the internal processing jobs run.
        /// <para>Default is every 2 seconds.</para>
        /// </summary>
        public TimeSpan InternalProcessingExecutionInterval { get; set; } = TimeSpan.FromSeconds(2);
    }
}