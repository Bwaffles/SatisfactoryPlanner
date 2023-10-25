using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DecreaseExtractionRate;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.IncreaseExtractionRate;
using SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.WorldNodes
{
    [TestFixture]
    public class DecreaseExtractionRateTests : TestBase
    {
        // Happy path tests
        [Test]
        public async Task WhenDataIsValid_IsSuccessful()
        {
            var (worldId, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule);

            await ResourcesModule.ExecuteCommandAsync(new IncreaseExtractionRateCommand(worldId, nodeId, 21));
            await ResourcesModule.ExecuteCommandAsync(new DecreaseExtractionRateCommand(worldId, nodeId, 10));

            var postTapNodeDetails =
                await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));
            postTapNodeDetails.ExtractionRate.Should().Be(10);
        }

        [Test]
        public async Task WhenExtractionRateIsZero_IsSuccessful()
        {
            var (worldId, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule);

            await ResourcesModule.ExecuteCommandAsync(new IncreaseExtractionRateCommand(worldId, nodeId, 21));
            await ResourcesModule.ExecuteCommandAsync(new DecreaseExtractionRateCommand(worldId, nodeId, 0));

            var postTapNodeDetails =
                await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));
            postTapNodeDetails.ExtractionRate.Should().Be(0);
        }

        // CommandValidator tests
        [Test]
        public void WhenWorldIdIsEmpty_ThrowsInvalidCommandException()
        {
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new DecreaseExtractionRateCommand(Guid.Empty, Guid.NewGuid(), 1));
            });
        }

        [Test]
        public void WhenNodeIdIsEmpty_ThrowsInvalidCommandException()
        {
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new DecreaseExtractionRateCommand(Guid.NewGuid(), Guid.Empty, 1));
            });
        }

        [Test]
        public async Task WhenExtractionRateIsNegative_ThrowsInvalidCommandException()
        {
            var (worldId, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule);

            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new DecreaseExtractionRateCommand(worldId, nodeId, -1));
            });
        }

        // Command Tests
        [Test]
        public async Task WhenWorldDoesNotExist_ThrowsInvalidCommandException()
        {
            var (_, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule);

            var differentWorldId = Guid.NewGuid();
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new DecreaseExtractionRateCommand(differentWorldId, nodeId, -1));
            });
        }

        [Test]
        public async Task WhenNodeDoesNotExist_ThrowsInvalidCommandException()
        {
            var (worldId, _) = await new TappedWorldNodeFixture().Create(ResourcesModule);

            var randomNodeId = Guid.NewGuid();
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new DecreaseExtractionRateCommand(worldId, randomNodeId, -1));
            });
        }
    }
}