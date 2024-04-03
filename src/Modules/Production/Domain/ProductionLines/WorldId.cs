using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines
{
    public record WorldId : TypedIdValueBase
    {
        public WorldId(Guid value) : base(value) { }
    }
}