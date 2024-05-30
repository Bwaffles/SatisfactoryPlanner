using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.SpawnWorldNodes;
using SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.WorldNodes
{
    [TestFixture]
    public class SpawnWorldNodesTests : IntegrationTest
    {
        // Happy path tests
        [Test]
        public async Task WhenDataIsValid_IsSuccessful()
        {
            var anonymousId = Guid.NewGuid();
            var worldId = Guid.NewGuid();

            (await ResourcesModule.ExecuteQueryAsync(new GetWorldNodesQuery(worldId, null)))
                .Should().BeEmpty();

            await ResourcesModule.ExecuteCommandAsync(new SpawnWorldNodesCommand(anonymousId, worldId));

            (await ResourcesModule.ExecuteQueryAsync(new GetWorldNodesQuery(worldId, null)))
                .Should().NotBeEmpty();
        }
    }
}