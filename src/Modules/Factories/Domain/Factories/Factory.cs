using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Domain.Factories
{
    public class Factory : Entity, IAggregateRoot
    {
        private FactoryId? _builtUnderFactoryId;
        private string _name;

        public FactoryId Id { get; }

        // ReSharper disable once UnusedMember.Local
        private Factory()
        { /* for EF */
            _name = default!;
            Id = default!;
        }

        private Factory(string name, FactoryId? builtUnderFactoryId)
        {
            Id = new FactoryId(Guid.NewGuid());
            _name = name;
            _builtUnderFactoryId = builtUnderFactoryId;
        }

        public static Factory Build(string name) => new(name, null);

        public Factory BuildSubFactory(string name) => new(name, Id);
    }
}