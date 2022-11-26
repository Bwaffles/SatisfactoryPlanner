using Autofac;
using SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers;

namespace SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration.Domain
{
    /// <summary>
    ///     Register the domain services for the module.
    /// </summary>
    internal class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder) =>
            builder.RegisterType<PioneersCounter>()
                .As<IPioneersCounter>()
                .InstancePerLifetimeScope();
    }
}