using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Extractors
{
    public class AllowedResource : Entity
    {
        internal ExtractorId ExtractorId { get; }

        internal ResourceId ResourceId { get; }

        private AllowedResource() { }

        internal bool Is(ResourceId resourceId) => ResourceId == resourceId;
    }
}
