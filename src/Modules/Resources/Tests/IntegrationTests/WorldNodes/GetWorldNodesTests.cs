using SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResources;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes;
using SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.WorldNodes
{
    [TestFixture]
    internal class GetWorldNodesTests : IntegrationTest
    {
        // Happy path tests
        [Test]
        public async Task WhenDataIsValid_IsSuccessful()
        {
            var worldId = await new WorldFixture().Create(ResourcesModule);
            var node = (await ResourcesModule.ExecuteQueryAsync(new GetWorldNodesQuery(worldId, null)))
                .WorldNodes
                .First(node => node.ResourceName == "Bauxite"
                               && node.Purity == "Pure"
                               && node.Biome == "Red Bamboo Fields"
                               && node.Number == 4);

            AssertAll(() =>
            {
                // Not sure how I feel about the node/resource part of this test
                // It requires some knowledge about a specific node, like that bauxite has 3 potential extractors
                node.Id.Should().Be(node.Id);
                node.Purity.Should().Be("Pure");
                node.Biome.Should().Be("Red Bamboo Fields");
                node.Number.Should().Be(4);
                node.ResourceId.Should().Be(node.ResourceId);
                node.ResourceName.Should().Be("Bauxite");
                node.IsTapped.Should().BeFalse();
                node.ExtractionRate.Should().Be(0);
                node.MaxExtractionRate.Should().Be(780);
                node.MapPositionX.Should().Be(-5292.68359M);
                node.MapPositionY.Should().Be(92075.19531M);
                node.MapPositionZ.Should().Be(22756.32617M);
            });
        }

        [Test]
        public async Task WhenFilteringByResourceId_ShouldReturnOnlyNodesForThatResource()
        {
            var worldId = await new WorldFixture().Create(ResourcesModule);

            var resource = (await ResourcesModule.ExecuteQueryAsync(new GetResourcesQuery(worldId)))
                .First(resource => resource.Name == "Bauxite");

            var worldNodes = (await ResourcesModule.ExecuteQueryAsync(new GetWorldNodesQuery(worldId, resource.Id)))
                .WorldNodes;

            AssertAll(() =>
            {
                worldNodes.Should().OnlyContain(worldNode => worldNode.ResourceId == resource.Id);
            });
        }

        [Test]
        public async Task WhenResourceIdDoesNotExist_NoWorldNodesFound()
        {
            var worldId = await new WorldFixture().Create(ResourcesModule);
            var resourceId = Guid.NewGuid();
            var worldNodes = (await ResourcesModule.ExecuteQueryAsync(new GetWorldNodesQuery(worldId, resourceId)))
                .WorldNodes;

            AssertAll(() =>
            {
                worldNodes.Should().BeEmpty();
            });
        }
    }
}