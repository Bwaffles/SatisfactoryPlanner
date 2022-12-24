using Moq;
using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes.Rules;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System;
using Xunit;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests
{
    public class TapNodeTests
    {
        [Fact]
        public void TapNode_WithOilExtractor_WhenAmountIsGreaterThanAvailableResources_IsNotPossible()
        {
            RuleAssertions.AssertBrokenRule<CannotExtractMoreThanTheAvailableResourcesRule>(() =>
            {
                new TapNodeExecuter()
                    .WithOilExtractor()
                    .ExtractTooManyResources()
                    .Execute();
            });
        }

        [Fact]
        public void TapNode_WithMiner_WhenAmountIsGreaterThanAvailableResources_IsNotPossible()
        {
            RuleAssertions.AssertBrokenRule<CannotExtractMoreThanTheAvailableResourcesRule>(() =>
            {
                new TapNodeExecuter()
                    .WithMiner()
                    .ExtractTooManyResources()
                    .Execute();
            });
        }

        [Fact]
        public void TapNode_WithMiner_WhenNodeIsAlreadyTapped_IsNotPossible()
        {
            RuleAssertions.AssertBrokenRule<NodeCannotAlreadyBeTappedRule>(() =>
            {
                new TapNodeExecuter()
                    .WithMiner()
                    .NodeAlreadyTapped()
                    .Execute();
            });
        }

        [Fact]
        public void TapNode_WithMiner_WhenExtractorCannotExtractTheResource_IsNotPossible()
        {
            RuleAssertions.AssertBrokenRule<ExtractorMustBeAbleToExtractResourceRule>(() =>
            {
                new TapNodeExecuter()
                    .WithMiner()
                    .CannotExtractResource()
                    .Execute();
            });
        }

        private class TapNodeExecuter
        {
            private static readonly decimal _amountExtractable = 600;
            private bool _canExtractResource = true;
            private string _extractorType = "Miner";
            private bool _extractTooManyResources;

            private bool _nodeTapped;

            public TappedNode Execute()
            {
                var resourceId = new ResourceId(Guid.NewGuid());

                var worldId = new WorldId(Guid.NewGuid());
                var extractor = GetExtractor(resourceId);
                var amountToExtract = GetAmountToExtract();
                var node = GetNode(resourceId);
                var tappedNodeExistenceChecker = GetTappedNodeExistenceChecked(node);

                return node.Tap(worldId, extractor, amountToExtract, "name", tappedNodeExistenceChecker);
            }

            private decimal GetAmountToExtract()
            {
                if (_extractorType == "OilExtractor")
                    return _extractTooManyResources ? 301 : 300;

                return _extractTooManyResources ? _amountExtractable + 1 : _amountExtractable;
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

            internal TapNodeExecuter ExtractTooManyResources()
            {
                _extractTooManyResources = true;
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
            {
                var extractorFixture = new ExtractorFixture();

                extractorFixture = _canExtractResource
                    ? extractorFixture.CanExtract(resourceId)
                    : extractorFixture.CannotExtract(resourceId);

                if (_extractorType == "OilExtractor")
                    extractorFixture = extractorFixture.AsOilExtractor();

                var extractor = extractorFixture.Build();
                return extractor;
            }

            private static Node GetNode(ResourceId resourceId) => new NodeFixture()
                .Of(resourceId)
                .WithPurity(NodePurity.Normal)
                .Build();

            private ITappedNodeExistenceChecker GetTappedNodeExistenceChecked(Node node)
            {
                var mockTappedNodeExistenceChecker = new Mock<ITappedNodeExistenceChecker>();
                mockTappedNodeExistenceChecker
                    .Setup(_ => _.IsTapped(node.Id.Value))
                    .Returns(_nodeTapped);
                var tappedNodeExistenceChecker = mockTappedNodeExistenceChecker.Object;
                return tappedNodeExistenceChecker;
            }
        }
    }
}