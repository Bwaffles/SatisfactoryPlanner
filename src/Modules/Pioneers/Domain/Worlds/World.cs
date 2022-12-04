using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Worlds.Events;

namespace SatisfactoryPlanner.Modules.Pioneers.Domain.Worlds
{
    public class World : Entity, IAggregateRoot
    {
        /// <summary>
        ///     The unique id of the <see cref="World" />.
        /// </summary>
        public WorldId Id { get; }

        private string _name;
        private PioneerId _creatorId;
        private DateTime _createDate;

        private World()
        { /* For EF */
        }

        private World(PioneerId creatorId, string name)
        {
            Id = new WorldId(Guid.NewGuid());
            _name = name;
            _creatorId = creatorId;
            _createDate = SystemClock.Now;

            AddDomainEvent(new WorldCreatedDomainEvent(Id, _name, _creatorId, _createDate));
        }

        public static World CreateStarterWorld(PioneerId creatorId)
        {
            return new World(creatorId, "Starter World");
        }
    }
}