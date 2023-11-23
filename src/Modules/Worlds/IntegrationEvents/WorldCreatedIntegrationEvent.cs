using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;

namespace SatisfactoryPlanner.Modules.Worlds.IntegrationEvents
{
    public class WorldCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid WorldId { get; }

        public WorldCreatedIntegrationEvent(Guid id, DateTime occurredOn, Guid worldId)
            : base(id, occurredOn) =>
            WorldId = worldId;
    }
}