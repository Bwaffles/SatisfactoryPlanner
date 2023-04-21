using Moq;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests
{
    internal class TapNodeExecuter
    {
        private bool _canExtractResource = true;
        private string _extractorType = "Miner";
        private bool _nodeTapped;

        public (TappedNode TappedNode, WorldId WorldId, NodeId NodeId, ExtractorId ExtractorId) Execute()
        {
            var resourceId = new ResourceId(Guid.NewGuid());
            var worldId = new WorldId(Guid.NewGuid());
            var extractor = GetExtractor(resourceId);
            var node = GetNode(resourceId);
            var tappedNodeExistenceChecker = GetTappedNodeExistenceChecked(node);

            var tappedNode = node.Tap(worldId, extractor, tappedNodeExistenceChecker);

            return (tappedNode,
                worldId,
                node.Id,
                extractor.Id);
        }

        internal TapNodeExecuter NodeAlreadyTapped()
        {
            _nodeTapped = true;
            return this;
        }

        internal TapNodeExecuter CannotExtractResource()
        {
            _canExtractResource = false;
            return this;
        }

        public TapNodeExecuter WithMiner()
        {
            _extractorType = "Miner";
            return this;
        }

        internal TapNodeExecuter WithOilExtractor()
        {
            _extractorType = "OilExtractor";
            return this;
        }

        private Extractor GetExtractor(ResourceId resourceId)
        { // TODO I don't think I need to test both miners and oil extractors here
            var extractorFixture = new ExtractorFixture();

            extractorFixture = _canExtractResource
                ? extractorFixture.CanExtract(resourceId)
                : extractorFixture.CannotExtract(resourceId);

            if (_extractorType == "OilExtractor")
                extractorFixture = extractorFixture.AsOilExtractor();

            return extractorFixture.Build();
        }

        private static Node GetNode(ResourceId resourceId) => new NodeFixture()
            .Of(resourceId)
            .WithPurity(NodePurity.Normal)
            .Build();

        private ITappedNodeExistenceChecker GetTappedNodeExistenceChecked(Node node)
        {
            var mockTappedNodeExistenceChecker = new Mock<ITappedNodeExistenceChecker>();
            mockTappedNodeExistenceChecker
                .Setup(_ => _.IsTapped(It.IsAny<Guid>(), node.Id.Value))
                .Returns(_nodeTapped);
            var tappedNodeExistenceChecker = mockTappedNodeExistenceChecker.Object;
            return tappedNodeExistenceChecker;
        }
    }
}