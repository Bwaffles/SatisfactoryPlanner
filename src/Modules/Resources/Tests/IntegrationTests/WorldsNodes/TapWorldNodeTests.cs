using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.TapWorldNode;
using SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.WorldsNodes
{
    [TestFixture]
    public class TapWorldNodeTests : TestBase
    {
        // Happy path tests
        [Test]
        public async Task WhenDataIsValid_IsSuccessful()
        {
            var worldId = await new WorldFixture().Create(ResourcesModule);

            var node = (await ResourcesModule.ExecuteQueryAsync(new GetWorldNodesQuery(worldId, null)))
                .First(_ => _.ResourceName == "Bauxite");
            var nodeDetails = await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, node.Id));
            nodeDetails.IsTapped.Should().BeFalse();

            var extractor = nodeDetails.AvailableExtractors.First();

            await ResourcesModule.ExecuteCommandAsync(new TapWorldNodeCommand(worldId, node.Id, extractor.Id));

            var postTapNodeDetails =
                await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, node.Id));
            postTapNodeDetails.IsTapped.Should().BeTrue();
            postTapNodeDetails.ExtractorId.Should().Be(extractor.Id);
        }

        // CommandValidator tests
        [Test]
        public void WhenWorldIdIsEmpty_ThrowsInvalidCommandException()
        {
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new TapWorldNodeCommand(Guid.Empty, Guid.NewGuid(), Guid.NewGuid()));
            });
        }

        [Test]
        public void WhenNodeIdIsEmpty_ThrowsInvalidCommandException()
        {
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new TapWorldNodeCommand(Guid.NewGuid(), Guid.Empty, Guid.NewGuid()));
            });
        }

        [Test]
        public void WhenExtractorIdIsEmpty_ThrowsInvalidCommandException()
        {
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new TapWorldNodeCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.Empty));
            });
        }

        // Command tests
        [Test]
        public async Task WhenWorldDoesNotExist_ThrowsInvalidCommandException()
        {
            var worldId = await new WorldFixture().Create(ResourcesModule);

            var node = (await ResourcesModule.ExecuteQueryAsync(new GetWorldNodesQuery(worldId, null)))
                .First(_ => _.ResourceName == "Bauxite");
            var extractor = (await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, node.Id)))
                .AvailableExtractors.First();

            var differentWorldId = Guid.NewGuid();
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(new TapWorldNodeCommand(differentWorldId, node.Id,
                    extractor.Id));
            });
        }

        [Test]
        public async Task WhenNodeDoesNotExist_ThrowsInvalidCommandException()
        {
            var worldId = await new WorldFixture().Create(ResourcesModule);

            var node = (await ResourcesModule.ExecuteQueryAsync(new GetWorldNodesQuery(worldId, null)))
                .First(_ => _.ResourceName == "Bauxite");
            var extractor = (await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, node.Id)))
                .AvailableExtractors.First();

            var randomNodeId = Guid.NewGuid();
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(new TapWorldNodeCommand(worldId, randomNodeId, extractor.Id));
            });
        }

        [Test]
        public async Task WhenExtractorDoesNotExist_ThrowsInvalidCommandException()
        {
            var worldId = await new WorldFixture().Create(ResourcesModule);

            var node = (await ResourcesModule.ExecuteQueryAsync(new GetWorldNodesQuery(worldId, null)))
                .First(_ => _.ResourceName == "Bauxite");

            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(new TapWorldNodeCommand(worldId, node.Id, Guid.NewGuid()));
            });
        }
    }
}