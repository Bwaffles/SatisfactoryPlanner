using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.WorldNodes
{
    internal class WorldNodeFixture
    {
        public (WorldNode worldNode, WorldId worldId, NodeId nodeId) Create()
        {
            var worldId = new WorldId(Guid.NewGuid());
            var nodeId = new NodeId(Guid.NewGuid());
            var worldNode = WorldNode.Spawn(worldId, nodeId);

            return (worldNode, worldId, nodeId);
        }
    }
}