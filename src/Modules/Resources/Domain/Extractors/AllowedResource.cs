using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Extractors
{
    public class AllowedResource : Entity
    {
        internal ExtractorId ExtractorId { get; }

        internal ResourceId ResourceId { get; }

        private AllowedResource(ExtractorId extractorId, ResourceId resourceId)
        {
            ExtractorId = extractorId;
            ResourceId = resourceId;
        }

        internal static AllowedResource CreateNew(ExtractorId extractorId, ResourceId resourceId)
            => new(extractorId, resourceId);

        internal bool Is(ResourceId resourceId) => ResourceId == resourceId;
    }
}