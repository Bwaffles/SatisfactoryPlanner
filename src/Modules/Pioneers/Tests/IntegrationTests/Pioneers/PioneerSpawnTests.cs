using Npgsql;
using SatisfactoryPlanner.Modules.Worlds.Application.Pioneers.GetPioneerDetails;
using SatisfactoryPlanner.Modules.Worlds.Application.Pioneers.SpawnPioneer;
using SatisfactoryPlanner.Modules.Worlds.Application.Worlds.CreateStarterWorld;
using SatisfactoryPlanner.Modules.Worlds.Application.Worlds.GetCurrentPioneerWorlds;
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

            // This can't really test if my query filter is wrong. At this point I've only added 1 world, so that's all i'm going to get.
            var starterWorld = (await WorldsModule.ExecuteQueryAsync(new GetCurrentPioneerWorldsQuery())).Single();

            starterWorld.Name.Should().Be("Starter World");

            // Trying to switch the "current pioneer" to see if no records get returned. I don't know if this is the right place, but want to capture this.
            ExecutionContext.UserId = Guid.NewGuid();
            var otherPioneerWorlds = await WorldsModule.ExecuteQueryAsync(new GetCurrentPioneerWorldsQuery());

            otherPioneerWorlds.Count.Should().Be(0);

            var connection = new NpgsqlConnection(ConnectionString);
            var message = (await OutboxMessagesHelper.GetOutboxMessages(connection)).Single();

            var worldCreatedNotification = OutboxMessagesHelper.Deserialize<WorldCreatedNotification>(message);
            worldCreatedNotification.DomainEvent.WorldId.Value.Should().Be(starterWorld.Id);
        }
    }
}