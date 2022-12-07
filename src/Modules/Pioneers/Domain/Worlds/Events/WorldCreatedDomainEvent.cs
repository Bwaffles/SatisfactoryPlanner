using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Worlds.Domain.Worlds.Events
{
    public class WorldCreatedDomainEvent : DomainEventBase
    {
        public WorldId WorldId { get; }

        public WorldCreatedDomainEvent(WorldId worldId)
        {
            WorldId = worldId;
        }
    }
}