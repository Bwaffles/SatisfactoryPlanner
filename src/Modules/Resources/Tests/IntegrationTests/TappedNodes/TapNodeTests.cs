using SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodes;
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

            var resources = await ResourcesModule.ExecuteQueryAsync(new GetResourcesQuery(worldId));
            resources
                .Should()
                .NotContain(resource => resource.ExtractedResources != 0);

            var bauxite = resources.First(resource => resource.Name == "Bauxite");

            var nodes = await ResourcesModule.ExecuteQueryAsync(new GetNodesQuery(worldId, bauxite.Id));
            nodes
                .Should()
                .OnlyContain(node => node.ResourceId == bauxite.Id);

            var node = nodes.First();

            // Tap node

            // Check the extracted resource count again
        }
    }
}