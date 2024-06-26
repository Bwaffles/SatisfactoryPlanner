using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources
{
    public sealed class Source : ValueObject
    {
        public SourceId Id { get; }

        public string Name { get; }

        public SourceType Type { get; }

        private Source(SourceId id, string name, SourceType type)
        {
            Id = id; 
            Name = name;
            Type = type;
        }

        public static Source Node(SourceId id, string name) => new(id, name, SourceType.Node);
    }
}
