using SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.GetPioneerDetails;
using SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.SpawnPioneer;
using SatisfactoryPlanner.Modules.Pioneers.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Pioneers.IntegrationTests.Pioneers
{
    [TestFixture]
    public class PioneerSpawnTests : TestBase
    {
        [Test]
        public async Task SpawnPioneer_Test()
        {
            var pioneerId = ExecutionContext.UserId;

            await PioneersModule.ExecuteCommandAsync(new SpawnPioneerCommand(
                Guid.NewGuid(),
                pioneerId
            ));

            var pioneerDetails = await PioneersModule.ExecuteQueryAsync(new GetPioneerDetailsQuery(pioneerId));

            pioneerDetails.Should().NotBeNull();
        }
    }
}