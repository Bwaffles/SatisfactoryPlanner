using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers;

namespace SatisfactoryPlanner.Modules.Pioneers.Domain.Worlds.Events
{
    public class WorldCreatedDomainEvent : DomainEventBase
    {
        public WorldId WorldId { get; }

        public string Name { get; }

        public PioneerId CreatorId { get; }

        public DateTime CreateDate { get; }

        public WorldCreatedDomainEvent(WorldId worldId, string name, PioneerId creatorId, DateTime createDate)
        {
            WorldId = worldId;
            Name = name;
            CreatorId = creatorId;
            CreateDate = createDate;
        }
    }
}