using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.UpgradeExtractor;
using SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.WorldNodes
{
    [TestFixture]
    public class UpgradeExtractorTests : TestBase
    {
        // Happy path tests
        [Test]
        public async Task WhenDataIsValid_IsSuccessful()
        {
            var (worldId, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule, extractorName: "Miner Mk.2");

            var worldNodeDetails =
                await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));

            var differentExtractorId = worldNodeDetails.AvailableExtractors
                .First(availableExtractor => availableExtractor.Name == "Miner Mk.3").Id;

            await ResourcesModule.ExecuteCommandAsync(
                new UpgradeExtractorCommand(worldId, nodeId, differentExtractorId));

            var postUpgradeDetails =
                await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));
            postUpgradeDetails.ExtractorId.Should().Be(differentExtractorId);
        }

        [Test]
        public async Task WhenAlreadyUsingGivenExtractor_IsSuccessful()
        {
            var (worldId, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule, extractorName: "Miner Mk.2");

            var worldNodeDetails =
                await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));

            var sameExtractorId = worldNodeDetails.ExtractorId!.Value;

            await ResourcesModule.ExecuteCommandAsync(
                new UpgradeExtractorCommand(worldId, nodeId, sameExtractorId));

            var postUpgradeDetails =
                await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));
            postUpgradeDetails.ExtractorId.Should().Be(sameExtractorId);
        }

        // CommandValidator tests
        [Test]
        public async Task WhenWorldIdIsEmpty_ThrowsInvalidCommandException()
        {
            var (worldId, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule, extractorName: "Miner Mk.2");

            var worldNodeDetails =
                await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));

            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new UpgradeExtractorCommand(Guid.Empty, nodeId, worldNodeDetails.ExtractorId!.Value));
            });
        }

        [Test]
        public async Task WhenNodeIdIsEmpty_ThrowsInvalidCommandException()
        {
            var (worldId, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule, extractorName: "Miner Mk.2");

            var worldNodeDetails =
                await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));

            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new UpgradeExtractorCommand(worldId, Guid.Empty, worldNodeDetails.ExtractorId!.Value));
            });
        }

        [Test]
        public async Task WhenExtractorIdIsEmpty_ThrowsInvalidCommandException()
        {
            var (worldId, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule, extractorName: "Miner Mk.2");

            var worldNodeDetails =
                await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new UpgradeExtractorCommand(worldId, nodeId, Guid.Empty));
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
                    new UpgradeExtractorCommand(randomWorldId, nodeId, Guid.NewGuid()));
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
                    new UpgradeExtractorCommand(worldId, randomNodeId, Guid.NewGuid()));
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
                    new UpgradeExtractorCommand(worldId, nodeId, randomExtractorId));
            });
        }
    }
}