using Autofac;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Infrastructure;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Infrastructure;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure;
using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;
using SatisfactoryPlanner.Modules.Warehouses.Infrastructure;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure;

namespace SatisfactoryPlanner.API.Configuration.Modules
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductionModule>()
                .As<IProductionModule>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ResourcesModule>()
                .As<IResourcesModule>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserAccessModule>()
                .As<IUserAccessModule>()
                .InstancePerLifetimeScope();

            builder.RegisterType<WarehousesModule>()
                .As<IWarehousesModule>()
                .InstancePerLifetimeScope();

            builder.RegisterType<WorldsModule>()
                .As<IWorldsModule>()
                .InstancePerLifetimeScope();
        }
    }
}
