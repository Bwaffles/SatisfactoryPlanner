using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes;
using SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.WorldNodes
{
    [TestFixture]
    internal class GetWorldNodeDetailsTests : IntegrationTest
    {
        // Happy path tests
        [Test]
        public async Task WhenDataIsValid_IsSuccessful()
        {
            var worldId = await new WorldFixture().Create(ResourcesModule);
            var node = (await ResourcesModule.ExecuteQueryAsync(new GetWorldNodesQuery(worldId, null)))
                .First(node => node.ResourceName == "Bauxite"
                               && node.Purity == "Pure"
                               && node.Biome == "Red Bamboo Fields"
                               && node.Number == 4);

            var result = await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, node.Id));
            var worldNodeDetails = result.Details;

            // Not sure how I feel about the node/resource part of this test
            // It requires some knowledge about a specific node, like that bauxite has 3 potential extractors
            worldNodeDetails.NodeId.Should().Be(node.Id);
            worldNodeDetails.Purity.Should().Be("Pure");
            worldNodeDetails.Biome.Should().Be("Red Bamboo Fields");
            worldNodeDetails.Number.Should().Be(4);
            worldNodeDetails.ResourceId.Should().Be(node.ResourceId);
            worldNodeDetails.ResourceName.Should().Be("Bauxite");
            worldNodeDetails.IsTapped.Should().BeFalse();
            worldNodeDetails.ExtractorId.Should().BeNull();
            worldNodeDetails.ExtractionRate.Should().Be(0);
            worldNodeDetails.AvailableExtractors.Should().HaveCount(3);
        }
    }
}