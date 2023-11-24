using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers
{
    public record PioneerId : TypedIdValueBase
    {
        public PioneerId(Guid value) : base(value) { }
    }
}