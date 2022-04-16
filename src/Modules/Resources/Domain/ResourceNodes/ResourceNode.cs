using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;

namespace SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodes
{
    public class ResourceNode : Entity, IAggregateRoot
    {
        public ResourceNodeId Id { get; }

        private readonly ResourceNodePurity _purity;

        private readonly ResourceId _resourceId;

        public static ResourceNode CreateNew(ResourceNodeId id, ResourceNodePurity purity, ResourceId resourceId)
        {
            return new(id, purity, resourceId);
        }

        private ResourceNode(ResourceNodeId id, ResourceNodePurity purity, ResourceId resourceId)
        {
            Id = id;
            _purity = purity;
            _resourceId = resourceId;
        }

        internal decimal GetPurityMultiplier() => _purity.GetMultiplier();

        internal ResourceId GetResourceId() => _resourceId;
    }
}
