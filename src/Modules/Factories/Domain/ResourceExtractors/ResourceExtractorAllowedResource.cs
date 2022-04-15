using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Factories.Domain.Resources;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ResourceExtractors
{
    public class ResourceExtractorAllowedResource : Entity
    {
        internal ResourceExtractorAllowedResourceId Id { get; }

        private ResourceId _resourceId;

        private ResourceExtractorId _resourceExtractorId;

        private ResourceExtractorAllowedResource() { }

        internal bool IsResource(ResourceId resourceId) => _resourceId == resourceId;
    }
}
