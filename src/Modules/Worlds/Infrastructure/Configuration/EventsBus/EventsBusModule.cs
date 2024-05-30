using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.EventsBus
{
    internal class EventsBusModule(IEventsBus eventsBus) : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(eventsBus).As<IEventsBus>();
        }
    }
}