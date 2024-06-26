using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources
{
    public record ItemSourceId : TypedIdValueBase
    {
        public ItemSourceId(Guid value) : base(value) { }
    }
}
