using Autofac;
using SatisfactoryPlanner.Modules.Resources.Application;
using SatisfactoryPlanner.Modules.Resources.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Domain
{
    /// <summary>
    ///     Register the domain services for the module.
    /// </summary>
    internal class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExtractionRateCalculator>()
                .As<IExtractionRateCalculator>()
                .InstancePerLifetimeScope();
        }
    }
}