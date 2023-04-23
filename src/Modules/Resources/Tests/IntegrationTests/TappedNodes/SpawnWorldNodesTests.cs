using SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodes;
using SatisfactoryPlanner.Modules.Resources.Application.TappedNodes.SpawnWorldNodes;
using SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.TappedNodes
{
    [TestFixture]
    public class SpawnWorldNodesTests : TestBase
    {
        [Test]
        public async Task IsSuccessful()
        {
            var anonymousId = Guid.NewGuid();
            var worldId = Guid.NewGuid();

            (await ResourcesModule.ExecuteQueryAsync(new GetNodesQuery(worldId, null)))
                .Should().BeEmpty();

            await ResourcesModule.ExecuteCommandAsync(new SpawnWorldNodesCommand(anonymousId, worldId));

            (await ResourcesModule.ExecuteQueryAsync(new GetNodesQuery(worldId, null)))
                .Should().NotBeEmpty();
        }
    }
}