using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Nodes
{
    public class Node : Entity, IAggregateRoot
    {
        private readonly NodePurity _purity;

        private readonly ResourceId _resourceId;

        public NodeId Id { get; }

        // ReSharper disable once UnusedMember.Local
        private Node()
        { /* for EF */
            _purity = default!;
            _resourceId = default!;
            Id = default!;
        }

        private Node(NodeId id, NodePurity purity, ResourceId resourceId)
        {
            Id = id;
            _purity = purity;
            _resourceId = resourceId;
        }

        public static Node CreateNew(NodeId id, NodePurity purity, ResourceId resourceId)
        {
            return new Node(id, purity, resourceId);
        }

        public decimal GetPurityMultiplier() => _purity.GetMultiplier();

        public ResourceId GetResourceId() => _resourceId;
    }
}