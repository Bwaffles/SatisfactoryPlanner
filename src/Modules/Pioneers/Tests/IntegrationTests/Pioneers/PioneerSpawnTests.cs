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
            var pioneerId = await PioneersModule.ExecuteCommandAsync(new SpawnPioneerCommand(
                "myAuth0UserId"
            ));

            pioneerId.Should().NotBeEmpty();

            var pioneerDetails = await PioneersModule.ExecuteQueryAsync(new GetPioneerDetailsQuery(pioneerId));

            pioneerDetails.Should().NotBeNull();
        }
    }
}