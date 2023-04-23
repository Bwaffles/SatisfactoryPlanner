using FluentAssertions;
using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System;
using Xunit;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests
{
    public class TapNodeTests
    {
        [Fact]
        public void TapNode_Succeeds()
        {
            var executionResult = new TapNodeExecuter().Execute();

            var domainEvent =
                DomainEventAssertions.AssertPublishedEvent<NodeTappedDomainEvent>(executionResult.WorldNode);
            domainEvent.ExtractorId.Should().Be(executionResult.ExtractorId);
            domainEvent.WorldId.Should().Be(executionResult.WorldId);
            domainEvent.NodeId.Should().Be(executionResult.NodeId);
        }

        [Fact]
        public void TapNode_WhenNodeIsAlreadyTapped_IsNotPossible()
        {
            var worldId = new WorldId(Guid.NewGuid());
            var nodeId = new NodeId(Guid.NewGuid());
            var worldNode = WorldNode.Spawn(worldId, nodeId);
            var extractorId = new ExtractorId(Guid.NewGuid());

            worldNode.Tap(extractorId);

            RuleAssertions.AssertBrokenRule<NodeCannotAlreadyBeTappedRule>(() =>
            {
                worldNode.Tap(extractorId);
            });
        }

        //[Fact]
        //public void TapNode_WhenExtractorCannotExtractTheResource_IsNotPossible()
        //{
        //    RuleAssertions.AssertBrokenRule<ExtractorMustBeAbleToExtractResourceRule>(() =>
        //    {
        //        new TapNodeExecuter()
        //            .CannotExtractResource()
        //            .Execute();
        //    });
        //}
    }
}