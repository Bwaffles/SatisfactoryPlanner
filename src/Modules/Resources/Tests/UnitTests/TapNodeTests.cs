using Moq;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions.Rules;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using System;
using Xunit;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests
{
    public class TapNodeTests
    {
        // TODO test for oil extractor

        [Fact]
        public void TapNode_WhenAmountIsGreaterThanAvailableResources_IsNotPossible()
        {
            Rules.AssertBrokenRule<CannotExtractMoreThanTheAvailableResourcesRule>(() =>
            {
                new TapNodeExecuter()
                    .ExtractTooManyResources()
                    .Execute();
            });
        }

        [Fact]
        public void TapNode_WhenNodeIsAlreadyTapped_IsNotPossible()
        {
            Rules.AssertBrokenRule<NodeCannotAlreadyBeTappedRule>(() =>
            {
                new TapNodeExecuter()
                    .NodeAlreadyTapped()
                    .Execute();
            });
        }

        [Fact]
        public void TapNode_WhenExtractorCannotExtractTheResource_IsNotPossible()
        {
            Rules.AssertBrokenRule<ExtractorMustBeAbleToExtractResourceRule>(() =>
            {
                new TapNodeExecuter()
                    .CannotExtractResource()
                    .Execute();
            });
        }

        private class TapNodeExecuter
        {
            private static readonly decimal _amountExtractable = 150;

            private bool _nodeTapped = false;
            private bool _canExtractResource = true;
            private decimal _amountToExtract = _amountExtractable;

            public TappedNode Execute()
            {
                var resourceId = new ResourceId(Guid.NewGuid());

                var node = GetNode(resourceId);
                var extractor = GetExtractor(resourceId);
                var tappedNodeExistenceChecker = GetTappedNodeExistenceChecked(node);

                return node.Tap(extractor, _amountToExtract, "name", tappedNodeExistenceChecker);
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
                _amountToExtract = _amountExtractable + 1;
                return this;
            }

            private Extractor GetExtractor(ResourceId resourceId)
            {
                var extractorFixture = new ExtractorFixture();

                if (_canExtractResource)
                    extractorFixture = extractorFixture.CanExtract(resourceId);
                else
                    extractorFixture = extractorFixture.CannotExtract(resourceId);

                var extractor = extractorFixture.Build();
                return extractor;
            }

            private static Node GetNode(ResourceId resourceId) => new NodeFixture()
                                .Of(resourceId)
                                .Build();

            private ITappedNodeExistenceChecker GetTappedNodeExistenceChecked(Domain.Nodes.Node node)
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
