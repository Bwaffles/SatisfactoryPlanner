using Autofac;
using SatisfactoryPlanner.BuildingBlocks.EventBus;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.EventsBus
{
    internal class EventsBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryEventBusClient>()
                .As<IEventsBus>()
                .SingleInstance();
        }
    }
}