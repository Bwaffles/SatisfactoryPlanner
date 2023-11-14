using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers;
using SatisfactoryPlanner.Modules.Worlds.Domain.Worlds.Events;

namespace SatisfactoryPlanner.Modules.Worlds.Domain.Worlds
{
    public class World : Entity, IAggregateRoot
    {
        private DateTime _createDate;
        private PioneerId _creatorId;
        private List<WorldInhabitant> _inhabitants;

        private string _name;

        /// <summary>
        ///     The unique id of the <see cref="World" />.
        /// </summary>
        public WorldId Id { get; }

        private World()
        { /* For EF */
            Id = default!;
            _name = default!;
            _inhabitants = default!;
            _creatorId = default!;
            _createDate = default!;
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