using Autofac;
using Serilog;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.Logging
{
    internal class LoggingModule(ILogger logger) : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(logger)
                .As<ILogger>()
                .SingleInstance();
        }
    }
}