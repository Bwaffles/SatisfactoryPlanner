using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Factories.Domain.Resources;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodes
{
    public class ResourceNode : Entity, IAggregateRoot
    {
        public ResourceNodeId Id { get; }

        private readonly ResourceNodePurity _purity;

        private readonly ResourceId _resourceId;

        private ResourceNode() { }

        internal decimal GetPurityMultiplier() => _purity.GetMultiplier();

        internal ResourceId GetResourceId() => _resourceId;
    }
}
