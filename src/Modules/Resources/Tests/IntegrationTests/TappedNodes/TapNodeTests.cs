using SatisfactoryPlanner.Modules.Resources.Application.Nodes;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodeDetails;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodes;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes.TapNode;
using SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResources;
using SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.TappedNodes
{
    [TestFixture]
    public class TapNodeTests : TestBase
    {
        [Test]
        public async Task TapNode_Test()
        {
            var worldId = Guid.NewGuid();
            
            var preTapResources = await ResourcesModule.ExecuteQueryAsync(new GetResourcesQuery(worldId));
            preTapResources.Should().OnlyContain(resource => resource.ExtractedResources == 0);

            var bauxite = preTapResources.First(resource => resource.Name == "Bauxite");

            var nodes = await ResourcesModule.ExecuteQueryAsync(new GetNodesQuery(worldId, bauxite.Id));
            nodes.Should().OnlyContain(node => node.ResourceId == bauxite.Id);

            var node = nodes.First();

            var nodeDetails = await ResourcesModule.ExecuteQueryAsync(new GetNodeDetailsQuery(worldId, node.Id));
            nodeDetails.Id.Should().Be(node.Id);

            var extractor = nodeDetails.AvailableExtractors.First(nodeDetail => nodeDetail.Name == "Miner Mk.1");

            await ResourcesModule.ExecuteCommandAsync(new TapNodeCommand(worldId, node.Id, extractor.Id));
            
            var postTapResources = await ResourcesModule.ExecuteQueryAsync(new GetResourcesQuery(worldId));
            postTapResources.Should().OnlyContain(resource => resource.ExtractedResources == 0);

            var postTapNodeDetails = await ResourcesModule.ExecuteQueryAsync(new GetNodeDetailsQuery(worldId, node.Id));
            postTapNodeDetails.IsTapped.Should().BeTrue();
            postTapNodeDetails.AmountToExtract.Should().Be(0);
        }

        [Test]
        public async Task TapNode_TappingNodesInDifferentWorldsShouldWork()
        {
            // This tests the query that checks if a node is already tapped to make sure it takes the world into account
            var worldId = Guid.NewGuid();

            var bauxite = (await ResourcesModule.ExecuteQueryAsync(new GetResourcesQuery(worldId)))
                .First(resource => resource.Name == "Bauxite");
            var node = (await ResourcesModule.ExecuteQueryAsync(new GetNodesQuery(worldId, bauxite.Id))).First();
            var nodeDetails = await ResourcesModule.ExecuteQueryAsync(new GetNodeDetailsQuery(worldId, node.Id));
            var extractor = nodeDetails.AvailableExtractors
                .First(nodeDetail => nodeDetail.Name == "Miner Mk.1");
            var tappedNodeId = await ResourcesModule.ExecuteCommandAsync(new TapNodeCommand(worldId, node.Id, extractor.Id));

            var differentWorldId = Guid.NewGuid();
            var differentTappedNodeId = await ResourcesModule.ExecuteCommandAsync(new TapNodeCommand(differentWorldId, node.Id, extractor.Id));

            tappedNodeId.Should().NotBe(differentTappedNodeId);
        }
    }
}