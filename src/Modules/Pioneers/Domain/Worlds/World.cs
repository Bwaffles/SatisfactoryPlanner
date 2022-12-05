using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers;
using SatisfactoryPlanner.Modules.Worlds.Domain.Worlds.Events;

namespace SatisfactoryPlanner.Modules.Worlds.Domain.Worlds
{
    public class World : Entity, IAggregateRoot
    {
        /// <summary>
        ///     The unique id of the <see cref="World" />.
        /// </summary>
        public WorldId Id { get; }

        private string _name;
        private List<WorldInhabitant> _inhabitants;
        private PioneerId _creatorId;
        private DateTime _createDate;

        private World()
        { /* For EF */
        }

        private World(PioneerId creatorId, string name)
        {
            Id = new WorldId(Guid.NewGuid());
            _name = name;
            _inhabitants = new List<WorldInhabitant>
            {
                WorldInhabitant.Create(Id, creatorId)
            };
            _creatorId = creatorId;
            _createDate = SystemClock.Now;

            AddDomainEvent(new WorldCreatedDomainEvent(Id));
        }

        public static World CreateStarterWorld(PioneerId creatorId)
        {
            return new World(creatorId, "Starter World");
        }
    }
}