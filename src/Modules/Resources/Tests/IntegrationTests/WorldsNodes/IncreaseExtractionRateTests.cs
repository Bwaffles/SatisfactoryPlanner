﻿using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodeDetails;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.IncreaseExtractionRate;
using SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.WorldsNodes
{
    [TestFixture]
    public class IncreaseExtractionRateTests : TestBase
    {
        // Happy path tests
        [Test]
        public async Task WhenDataIsValid_IsSuccessful()
        {
            var (worldId, nodeId) = await new WorldNodeFixture().Create(ResourcesModule);

            await ResourcesModule.ExecuteCommandAsync(new IncreaseExtractionRateCommand(worldId, nodeId, 21));

            var postTapNodeDetails = await ResourcesModule.ExecuteQueryAsync(new GetNodeDetailsQuery(worldId, nodeId));
            postTapNodeDetails.ExtractionRate.Should().Be(21);
        }

        // CommandValidator tests
        [Test]
        public void WhenWorldIdIsEmpty_ThrowsInvalidCommandException()
        {
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new IncreaseExtractionRateCommand(Guid.Empty, Guid.NewGuid(), 1));
            });
        }

        [Test]
        public void WhenNodeIdIsEmpty_ThrowsInvalidCommandException()
        {
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new IncreaseExtractionRateCommand(Guid.NewGuid(), Guid.Empty, 1));
            });
        }

        [Test]
        public async Task WhenExtractionRateIsZero_ThrowsInvalidCommandException()
        {
            var (worldId, nodeId) = await new WorldNodeFixture().Create(ResourcesModule);

            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new IncreaseExtractionRateCommand(worldId, nodeId, 0));
            });
        }

        [Test]
        public async Task WhenExtractionRateIsNegative_ThrowsInvalidCommandException()
        {
            var (worldId, nodeId) = await new WorldNodeFixture().Create(ResourcesModule);

            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new IncreaseExtractionRateCommand(worldId, nodeId, -1));
            });
        }

        // Command Tests
        [Test]
        public async Task WhenWorldDoesNotExist_ThrowsInvalidCommandException()
        {
            var (_, nodeId) = await new WorldNodeFixture().Create(ResourcesModule);

            var differentWorldId = Guid.NewGuid();
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new IncreaseExtractionRateCommand(differentWorldId, nodeId, -1));
            });
        }

        [Test]
        public async Task WhenNodeDoesNotExist_ThrowsInvalidCommandException()
        {
            var (worldId, _) = await new WorldNodeFixture().Create(ResourcesModule);

            var randomNodeId = Guid.NewGuid();
            Assert.CatchAsync<InvalidCommandException>(async () =>
            {
                await ResourcesModule.ExecuteCommandAsync(
                    new IncreaseExtractionRateCommand(worldId, randomNodeId, -1));
            });
        }
    }
}