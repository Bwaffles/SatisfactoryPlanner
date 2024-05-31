using Autofac;
using Serilog;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Logging
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