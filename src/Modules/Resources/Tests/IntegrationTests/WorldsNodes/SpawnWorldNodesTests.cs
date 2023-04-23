using SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodes;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.SpawnWorldNodes;
using SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.WorldsNodes
{
    [TestFixture]
    public class SpawnWorldNodesTests : TestBase
    {
        // Happy path tests
        [Test]
        public async Task WhenDataIsValid_IsSuccessful()
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