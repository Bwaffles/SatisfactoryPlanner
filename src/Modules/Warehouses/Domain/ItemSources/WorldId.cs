using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources
{
    public record WorldId : TypedIdValueBase
    {
        public WorldId(Guid value) : base(value) { }
    }
}
