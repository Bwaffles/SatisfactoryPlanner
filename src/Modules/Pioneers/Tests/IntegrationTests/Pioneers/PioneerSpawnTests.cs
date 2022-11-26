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
            var id = await PioneersModule.ExecuteCommandAsync(new SpawnPioneerCommand(
                "myAuth0UserId"
            ));

            id.Should().NotBeEmpty();

            // TODO write a query to retrieve the pioneer
        }
    }
}