using Autofac;
using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;
using SatisfactoryPlanner.Modules.Warehouses.Infrastructure;

namespace SatisfactoryPlanner.API.Configuration.Modules
{
    public class WarehousesAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder) =>
            builder.RegisterType<WarehousesModule>()
                .As<IWarehousesModule>()
                .InstancePerLifetimeScope();
    }
}