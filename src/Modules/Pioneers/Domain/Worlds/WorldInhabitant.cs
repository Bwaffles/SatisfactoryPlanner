using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers;

namespace SatisfactoryPlanner.Modules.Worlds.Domain.Worlds
{
    public class WorldInhabitant : Entity
    {
        internal WorldId WorldId { get; }

        internal PioneerId PioneerId { get; }

        private WorldInhabitant()
        { /* For EF */
            WorldId = default!;
            PioneerId = default!;
        }

        private WorldInhabitant(WorldId worldId, PioneerId pioneerId)
        {
            WorldId = worldId;
            PioneerId = pioneerId;
        }

        public static WorldInhabitant Create(WorldId worldId, PioneerId pioneerId)
        {
            return new WorldInhabitant(worldId, pioneerId);
        }
    }
}