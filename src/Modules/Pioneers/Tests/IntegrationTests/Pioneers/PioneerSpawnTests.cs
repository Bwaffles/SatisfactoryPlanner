using SatisfactoryPlanner.Modules.Worlds.Application.Pioneers.GetPioneerDetails;
using SatisfactoryPlanner.Modules.Worlds.Application.Pioneers.SpawnPioneer;
using SatisfactoryPlanner.Modules.Worlds.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Worlds.IntegrationTests.Pioneers
{
    [TestFixture]
    public class PioneerSpawnTests : TestBase
    {
        [Test]
        public async Task SpawnPioneer_Test()
        {
            var pioneerId = ExecutionContext.UserId;

            await WorldsModule.ExecuteCommandAsync(new SpawnPioneerCommand(
                Guid.NewGuid(),
                pioneerId
            ));

            var pioneerDetails = await WorldsModule.ExecuteQueryAsync(new GetPioneerDetailsQuery(pioneerId));

            pioneerDetails.Should().NotBeNull();

            // Get world?
        }
    }
}