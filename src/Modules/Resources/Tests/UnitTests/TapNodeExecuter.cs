using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests
{
    internal class TapNodeExecuter
    {
        private bool _canExtractResource = true;

        public (WorldNode WorldNode, WorldId WorldId, NodeId NodeId, ExtractorId ExtractorId) Execute()
        {
            var resourceId = new ResourceId(Guid.NewGuid());
            var worldId = new WorldId(Guid.NewGuid());
            var node = GetNode(resourceId);
            var worldNode = WorldNode.Spawn(worldId, node.Id);
            var extractor = GetExtractor(resourceId);

            worldNode.Tap(extractor.Id);

            return (worldNode,
                worldId,
                node.Id,
                extractor.Id);
        }

        internal TapNodeExecuter CannotExtractResource()
        {
            _canExtractResource = false;
            return this;
        }

        private Extractor GetExtractor(ResourceId resourceId)
        {
            var extractorFixture = new ExtractorFixture();

            extractorFixture = _canExtractResource
                ? extractorFixture.CanExtract(resourceId)
                : extractorFixture.CannotExtract(resourceId);

            return extractorFixture.Build();
        }

        private static Node GetNode(ResourceId resourceId) => new NodeFixture()
            .Of(resourceId)
            .WithPurity(NodePurity.Normal)
            .Build();
    }
}