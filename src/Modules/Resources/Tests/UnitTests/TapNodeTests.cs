using FluentAssertions;
using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes.Rules;
using Xunit;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests
{
    public class TapNodeTests
    {
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

        [Fact]
        public void TapNode_WithOilExtractor_WhenExtractorCannotExtractTheResource_IsNotPossible()
        {
            RuleAssertions.AssertBrokenRule<ExtractorMustBeAbleToExtractResourceRule>(() =>
            {
                new TapNodeExecuter()
                    .WithOilExtractor()
                    .CannotExtractResource()
                    .Execute();
            });
        }

        [Fact]
        public void TapNode_Succeeds()
        {
            var executionResult = new TapNodeExecuter().Execute();

            var domainEvent =
                DomainEventAssertions.AssertPublishedEvent<NodeTappedDomainEvent>(executionResult.TappedNode);
            domainEvent.ExtractorId.Should().Be(executionResult.ExtractorId);
            domainEvent.WorldId.Should().Be(executionResult.WorldId);
            domainEvent.NodeId.Should().Be(executionResult.NodeId);
        }
    }
}