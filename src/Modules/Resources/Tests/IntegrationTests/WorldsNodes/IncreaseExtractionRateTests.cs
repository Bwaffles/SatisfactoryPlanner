namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.WorldsNodes
{
    //[TestFixture]
    //public class IncreaseExtractionRateTests : TestBase
    //{
    //    [Test]
    //    public async Task WhenDataIsValid_IsSuccessful()
    //    {
    //        var worldId = Guid.NewGuid();
    //        var (_, nodeId, _) = await new TappedNodeFixture()
    //            .WithWorldId(worldId)
    //            .CreateTappedNode(ResourcesModule);

    //        await ResourcesModule.ExecuteCommandAsync(new IncreaseExtractionRateCommand(worldId, nodeId, 21));

    //        var postTapNodeDetails = await ResourcesModule.ExecuteQueryAsync(new GetNodeDetailsQuery(worldId, nodeId));
    //        postTapNodeDetails.ExtractionRate.Should().Be(21);
    //    }

    //    // CommandValidator Tests
    //    [Test]
    //    public void WhenWorldIdIsEmpty_ThrowsInvalidCommandException()
    //    {
    //        Assert.CatchAsync<InvalidCommandException>(async () =>
    //        {
    //            await ResourcesModule.ExecuteCommandAsync(
    //                new IncreaseExtractionRateCommand(Guid.Empty, Guid.NewGuid(), 1));
    //        });
    //    }

    //    [Test]
    //    public void WhenNodeIdIsEmpty_ThrowsInvalidCommandException()
    //    {
    //        Assert.CatchAsync<InvalidCommandException>(async () =>
    //        {
    //            await ResourcesModule.ExecuteCommandAsync(
    //                new IncreaseExtractionRateCommand(Guid.NewGuid(), Guid.Empty, 1));
    //        });
    //    }

    //    [Test]
    //    public async Task WhenExtractionRateIsZero_ThrowsInvalidCommandException()
    //    {
    //        var (worldId, nodeId, _) = await new TappedNodeFixture()
    //            .CreateTappedNode(ResourcesModule);

    //        Assert.CatchAsync<InvalidCommandException>(async () =>
    //        {
    //            await ResourcesModule.ExecuteCommandAsync(
    //                new IncreaseExtractionRateCommand(worldId, nodeId, 0));
    //        });
    //    }

    //    [Test]
    //    public async Task WhenNewExtractionRateIsNegative_ThrowsInvalidCommandException()
    //    {
    //        var (worldId, nodeId, _) = await new TappedNodeFixture()
    //            .CreateTappedNode(ResourcesModule);

    //        Assert.CatchAsync<InvalidCommandException>(async () =>
    //        {
    //            await ResourcesModule.ExecuteCommandAsync(
    //                new IncreaseExtractionRateCommand(worldId, nodeId, -1));
    //        });
    //    }

    //    // Command Tests
    //    [Test]
    //    public void WhenTappedNodeDoesNotExist_ThrowsInvalidCommandException()
    //    {
    //        Assert.CatchAsync<InvalidCommandException>(async () =>
    //        {
    //            await ResourcesModule.ExecuteCommandAsync(
    //                new IncreaseExtractionRateCommand(Guid.NewGuid(), Guid.NewGuid(), 1));
    //        });
    //    }

    //    [Test]
    //    public async Task WhenTappedNodeIsInWrongWorld_ThrowsInvalidCommandException()
    //    {
    //        var myWorldId = Guid.NewGuid();
    //        var (_, nodeId, _) = await new TappedNodeFixture()
    //            .WithWorldId(myWorldId)
    //            .CreateTappedNode(ResourcesModule);

    //        var otherPioneersWorldId = Guid.NewGuid();
    //        Assert.CatchAsync<InvalidCommandException>(async () =>
    //        {
    //            await ResourcesModule.ExecuteCommandAsync(
    //                new IncreaseExtractionRateCommand(otherPioneersWorldId, nodeId, 1));
    //        });
    //    }
    //}
}