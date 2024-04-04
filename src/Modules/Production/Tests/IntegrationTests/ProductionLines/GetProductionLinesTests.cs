using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLines;
using SatisfactoryPlanner.Modules.Production.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Production.IntegrationTests.ProductionLines
{
    [TestFixture]
    internal class GetProductionLinesTests : TestBase
    {
        // Happy path tests
        [Test]
        public async Task WhenDataIsValid_IsSuccessful()
        {
            // TODO test that world filter is working once I can set up new production lines
            var worldId = Guid.NewGuid();
            var productionLines = (await ProductionModule.ExecuteQueryAsync(new GetProductionLinesQuery(worldId)));

            productionLines.Should().BeEmpty();
        }
    }
}
