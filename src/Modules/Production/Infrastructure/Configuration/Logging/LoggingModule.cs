using Autofac;
using Serilog;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Logging
{
    /// <summary>
    ///     Dependency injection setup for Logging of the Factories module.
    ///     This registers the classes that handle the logging for this module.
    ///     Should be called from the module Startup.
    /// </summary>
    internal class LoggingModule : Module
    {
        private readonly ILogger _logger;

        internal LoggingModule(ILogger logger)
        {
            _logger = logger;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_logger)
                .As<ILogger>()
                .SingleInstance();
        }
    }
}