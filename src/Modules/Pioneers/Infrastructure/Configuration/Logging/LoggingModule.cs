using Autofac;
using Serilog;

namespace SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration.Logging
{
    // TODO can we just centralize this in building blocks?
    internal class LoggingModule : Module
    {
        private readonly ILogger _logger;

        internal LoggingModule(ILogger logger) => _logger = logger;

        protected override void Load(ContainerBuilder builder) =>
            builder.RegisterInstance(_logger)
                .As<ILogger>()
                .SingleInstance();
    }
}