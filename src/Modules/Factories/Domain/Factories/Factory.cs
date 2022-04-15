using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Factories.Domain.Factories
{
    public class Factory : Entity, IAggregateRoot
    {
        public FactoryId Id { get; private set; }

        private string _name { get; }

        private FactoryId? _builtUnderFactoryId { get; }

        private Factory() { }

        private Factory(string name, FactoryId? builtUnderFactoryId)
        {
            Id = new FactoryId(System.Guid.NewGuid());
            _name = name;
            _builtUnderFactoryId = builtUnderFactoryId;
        }

        public static Factory Build(string name)
            => new Factory(name, builtUnderFactoryId: null);

        public Factory BuildSubFactory(string name)
            => new Factory(name, Id);
    }
}
