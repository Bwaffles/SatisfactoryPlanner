using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Factories.Domain.Factories
{
    public class Factory : Entity, IAggregateRoot
    {
        public FactoryId Id { get; private set; }

        private string _name { get; }

        private Factory() { }

        private Factory(string name)
        {
            Id = new FactoryId(System.Guid.NewGuid());
            _name = name;
        }

        public static Factory BuildNew(string name)
        {
            return new Factory(name);
        }
    }
}
