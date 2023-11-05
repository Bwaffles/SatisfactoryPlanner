using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using SatisfactoryPlanner.Modules.Resources.UnitTests.Extractors;
using System;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.WorldNodes
{
    internal class WorldNodeTestData
    {
        public WorldNode WorldNode { get; }

        public WorldId WorldId { get; }

        public NodeId NodeId { get; }

        public ResourceId ResourceId { get; }

        public Extractor? Extractor { get; }

        public WorldNodeTestData(WorldNode worldNode, WorldId worldId, NodeId nodeId, ResourceId resourceId,
            Extractor? extractor)
        {
            WorldNode = worldNode;
            WorldId = worldId;
            NodeId = nodeId;
            ResourceId = resourceId;
            Extractor = extractor;
        }
    }

    internal class WorldNodeFixture
    {
        private Extractor? _extractor;
        private ResourceId? _resourceId;
        private bool _tapped;

        public WorldNodeFixture IsTapped()
        {
            _tapped = true;
            return this;
        }

        public WorldNodeFixture IsTapped(Extractor extractor, ResourceId resourceId)
        {
            _tapped = true;
            _extractor = extractor;
            _resourceId = resourceId;
            return this;
        }

        public WorldNodeTestData Create()
        {
            var worldId = new WorldId(Guid.NewGuid());
            var nodeId = new NodeId(Guid.NewGuid());
            var worldNode = WorldNode.Spawn(worldId, nodeId);

            var resourceId = _resourceId ?? new ResourceId(Guid.NewGuid());
            var extractor = _extractor ?? new ExtractorFixture().CanExtract(resourceId).Create();

            if (_tapped)
                worldNode.Tap(extractor, resourceId);

            return new WorldNodeTestData(worldNode, worldId, nodeId, resourceId, _tapped ? extractor : null);
        }
    }
}