using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using System;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.Nodes
{
    internal class NodeFixture
    {
        private NodePurity _purity = NodePurity.Normal;
        private ResourceId _resourceId = new(Guid.NewGuid());

        internal Node Build()
        {
            var id = new NodeId(Guid.NewGuid());
            return Node.CreateNew(id, _purity, _resourceId);
        }

        internal NodeFixture Of(ResourceId resourceId)
        {
            _resourceId = resourceId;
            return this;
        }

        internal NodeFixture WithPurity(NodePurity purity)
        {
            _purity = purity;
            return this;
        }
    }
}