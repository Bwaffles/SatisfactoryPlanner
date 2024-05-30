using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DowngradeExtractor;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails;
using SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.WorldNodes
{
    [TestFixture]
    public class DowngradeExtractorTests : IntegrationTest
    {
        // Happy path tests
        [Test]
        public async Task WhenDataIsValid_IsSuccessful()
        {
            var (worldId, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule, "Miner Mk.2");

            var worldNodeDetails =
                await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));

            var slowerExtractorId = worldNodeDetails.AvailableExtractors
                .First(availableExtractor => availableExtractor.Name == "Miner Mk.1").Id;

            await ResourcesModule.ExecuteCommandAsync(
                new DowngradeExtractorCommand(worldId, nodeId, slowerExtractorId));

            var postUpgradeDetails =
                await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));
            postUpgradeDetails.ExtractorId.Should().Be(slowerExtractorId);
        }

        [Test]
        public async Task WhenAlreadyUsingGivenExtractor_IsSuccessful()
        {
            var (worldId, nodeId, extractorId) =
                await new TappedWorldNodeFixture().Create(ResourcesModule, "Miner Mk.2");

            await ResourcesModule.ExecuteCommandAsync(
                new DowngradeExtractorCommand(worldId, nodeId, extractorId));

            var postUpgradeDetails =
                await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));
            postUpgradeDetails.ExtractorId.Should().Be(extractorId);
        }

        // CommandValidator tests
        [Test]
        public async Task WhenWorldIdIsEmpty_ThrowsInvalidCommandException()
        {
            var (_, nodeId, extractorId) = await new TappedWorldNodeFixture().Create(ResourcesModule, "Miner Mk.2");

            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new DowngradeExtractorCommand(Guid.Empty, nodeId, extractorId));
            });
        }

        [Test]
        public async Task WhenNodeIdIsEmpty_ThrowsInvalidCommandException()
        {
            var (worldId, _, extractorId) = await new TappedWorldNodeFixture().Create(ResourcesModule, "Miner Mk.2");

            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new DowngradeExtractorCommand(worldId, Guid.Empty, extractorId));
            });
        }

        [Test]
        public async Task WhenExtractorIdIsEmpty_ThrowsInvalidCommandException()
        {
            var (worldId, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule, "Miner Mk.2");

            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new DowngradeExtractorCommand(worldId, nodeId, Guid.Empty));
            });
        }

        // Command Tests
        [Test]
        public async Task WhenWorldDoesNotExist_ThrowsInvalidCommandException()
        {
            var (_, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule);

            var randomWorldId = Guid.NewGuid();
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new DowngradeExtractorCommand(randomWorldId, nodeId, Guid.NewGuid()));
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
                    new DowngradeExtractorCommand(worldId, randomNodeId, Guid.NewGuid()));
            });
        }

        [Test]
        public async Task WhenExtractorDoesNotExist_ThrowsInvalidCommandException()
        {
            var (worldId, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule);

            var randomExtractorId = Guid.NewGuid();
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new DowngradeExtractorCommand(worldId, nodeId, randomExtractorId));
            });
        }
    }
}