using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLines;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.SetUpProductionLine;
using SatisfactoryPlanner.Modules.Production.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Production.IntegrationTests.ProductionLines
{
    [TestFixture]
    internal class GetProductionLinesTests : IntegrationTest
    {
        // Happy path tests
        [Test]
        public async Task CanFilterByWorld()
        {
            var world1 = Guid.NewGuid();
            await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(world1, "World 1 Rocky Desert Iron Ingots - Line 1"));

            var world2 = Guid.NewGuid();
            await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(world2, "World 2 Rocky Desert Iron Ingots - Line 1"));

            var world1ProductionLines = (await ProductionModule.ExecuteQueryAsync(new GetProductionLinesQuery(world1)));

            var world1ProductionLine = world1ProductionLines.Single();
            world1ProductionLine.Name.Should().Be("World 1 Rocky Desert Iron Ingots - Line 1");

            var world2ProductionLines = (await ProductionModule.ExecuteQueryAsync(new GetProductionLinesQuery(world2)));

            var world2ProductionLine = world2ProductionLines.Single();
            world2ProductionLine.Name.Should().Be("World 2 Rocky Desert Iron Ingots - Line 1");
        }
    }
}
