using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Worlds.Domain.Worlds
{
    public record WorldId : TypedIdValueBase
    {
        public WorldId(Guid value) : base(value) { }
    }
}