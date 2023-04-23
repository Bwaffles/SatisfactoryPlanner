using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using SatisfactoryPlanner.Modules.Resources.UnitTests.Extractors;
using System;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.WorldNodes
{
    internal class TapNodeExecuter
    {
        private readonly bool _canExtractResource = true;

        public (WorldNode WorldNode, WorldId WorldId, NodeId NodeId, ExtractorId ExtractorId) Execute()
        {
            var (worldNode, worldId, nodeId) = new WorldNodeFixture().Create();
            var resourceId = new ResourceId(Guid.NewGuid());
            var extractor = GetExtractor(resourceId);

            worldNode.Tap(extractor, resourceId);

            return (worldNode,
                worldId,
                nodeId,
                extractor.Id);
        }

        private Extractor GetExtractor(ResourceId resourceId)
        {
            var extractorFixture = new ExtractorFixture();

            extractorFixture = _canExtractResource
                ? extractorFixture.CanExtract(resourceId)
                : extractorFixture.CannotExtract(resourceId);

            return extractorFixture.Create();
        }
    }
}