using Autofac;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure;

namespace SatisfactoryPlanner.API.Configuration.Modules
{
    public class WorldsAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder) =>
            builder.RegisterType<WorldsModule>()
                .As<IWorldsModule>()
                .InstancePerLifetimeScope();
    }
}