using Autofac;
using SatisfactoryPlanner.Modules.Pioneers.Application.Contracts;
using SatisfactoryPlanner.Modules.Pioneers.Infrastructure;

namespace SatisfactoryPlanner.API.Modules.Pioneers
{
    public class PioneersAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder) =>
            builder.RegisterType<PioneersModule>()
                .As<IPioneersModule>()
                .InstancePerLifetimeScope();
    }
}