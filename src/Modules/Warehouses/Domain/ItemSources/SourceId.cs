using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources
{
    public record SourceId : TypedIdValueBase
    {
        public SourceId(Guid value) : base(value) { }
    }
}
