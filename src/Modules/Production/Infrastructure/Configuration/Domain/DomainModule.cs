using Autofac;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.SetUpProductionLine;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Domain
{
    /// <summary>
    ///     Register the domain services for the module.
    /// </summary>
    internal class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductionLineCounter>()
                .As<IProductionLineCounter>()
                .InstancePerLifetimeScope();
        }
    }
}
