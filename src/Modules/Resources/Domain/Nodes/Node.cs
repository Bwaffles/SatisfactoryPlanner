using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Nodes
{
    public class Node : Entity, IAggregateRoot
    {
        public NodeId Id { get; }

        private readonly NodePurity _purity;

        private readonly ResourceId _resourceId;

        public static Node CreateNew(NodeId id, NodePurity purity, ResourceId resourceId)
        {
            return new(id, purity, resourceId);
        }

        private Node(NodeId id, NodePurity purity, ResourceId resourceId)
        {
            Id = id;
            _purity = purity;
            _resourceId = resourceId;
        }

        internal decimal GetPurityMultiplier() => _purity.GetMultiplier();

        internal ResourceId GetResourceId() => _resourceId;
    }
}
