using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DismantleExtractor;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails;
using SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.WorldNodes
{
    [TestFixture]
    public class DismantleExtractorTests : IntegrationTest
    {
        // Happy path tests
        [Test]
        public async Task WhenDataIsValid_IsSuccessful()
        {
            var (worldId, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule);

            await ResourcesModule.ExecuteCommandAsync(new DismantleExtractorCommand(worldId, nodeId));

            var postDismantleDetails =
                await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));
            postDismantleDetails.IsTapped.Should().BeFalse();
            postDismantleDetails.ExtractorId.Should().BeNull();
            postDismantleDetails.ExtractionRate.Should().Be(0);
        }

        // CommandValidator tests
        [Test]
        public async Task WhenWorldIdIsEmpty_ThrowsInvalidCommandException()
        {
            var (_, nodeId) = await new TappedWorldNodeFixture().Create(ResourcesModule);

            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(new DismantleExtractorCommand(Guid.Empty, nodeId));
            });
        }

        [Test]
        public async Task WhenNodeIdIsEmpty_ThrowsInvalidCommandException()
        {
            var (worldId, _) = await new TappedWorldNodeFixture().Create(ResourcesModule);

            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(new DismantleExtractorCommand(worldId, Guid.Empty));
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
                    new DismantleExtractorCommand(randomWorldId, nodeId));
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
                    new DismantleExtractorCommand(worldId, randomNodeId));
            });
        }
    }
}