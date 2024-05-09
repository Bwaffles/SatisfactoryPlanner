using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLineDetails;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.SetUpProductionLine;
using SatisfactoryPlanner.Modules.Production.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Production.IntegrationTests.ProductionLines
{
    [TestFixture]
    internal class GetProductionLineDetailsTests : IntegrationTest
    {
        [Test]
        public async Task WhenDataIsValid_IsSuccessful()
        {
            var world = Guid.NewGuid();
            var productionLineId = await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(world, "Rocky Desert Iron Ingots - Line 1"));

            var productionLineDetails = (await ProductionModule.ExecuteQueryAsync(new GetProductionLineDetailsQuery(world, productionLineId)));

            productionLineDetails.Should().NotBeNull();
            productionLineDetails!.Id.Should().Be(productionLineId);
            productionLineDetails!.Name.Should().Be("Rocky Desert Iron Ingots - Line 1");
        }

        [Test]
        public async Task WhenProductionLineIdNotFound_ShouldReturnNull()
        {
            var world = Guid.NewGuid();
            var nonExistentProductionLineId = Guid.NewGuid();

            var productionLineDetails = (await ProductionModule.ExecuteQueryAsync(new GetProductionLineDetailsQuery(world, nonExistentProductionLineId)));
            productionLineDetails.Should().BeNull();
        }

        [Test]
        public async Task WhenProductionLineIsNotInWorld_ShouldReturnNull()
        {
            var world1 = Guid.NewGuid();
            var productionLineId1 = await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(world1, "World 1 Rocky Desert Iron Ingots - Line 1"));

            var world2 = Guid.NewGuid();
            _ = await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(world2, "World 2 Rocky Desert Iron Ingots - Line 1"));

            var productionLineDetails = (await ProductionModule.ExecuteQueryAsync(new GetProductionLineDetailsQuery(world2, productionLineId1)));
            productionLineDetails.Should().BeNull();
        }
    }
}
