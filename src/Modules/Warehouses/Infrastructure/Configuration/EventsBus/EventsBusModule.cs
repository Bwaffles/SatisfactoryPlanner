using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.EventsBus
{
    internal class EventsBusModule(IEventsBus eventsBus) : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(eventsBus).As<IEventsBus>();
        }
    }
}