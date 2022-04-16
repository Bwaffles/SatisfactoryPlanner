using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;

namespace SatisfactoryPlanner.Modules.Resources.Domain.ResourceExtractors
{
    public class ResourceExtractorAllowedResource : Entity
    {
        internal ResourceExtractorAllowedResourceId Id { get; }

        private readonly ResourceId _resourceId;

        private readonly ResourceExtractorId _resourceExtractorId;

        private ResourceExtractorAllowedResource() { }

        internal bool IsResource(ResourceId resourceId) => _resourceId == resourceId;
    }
}
