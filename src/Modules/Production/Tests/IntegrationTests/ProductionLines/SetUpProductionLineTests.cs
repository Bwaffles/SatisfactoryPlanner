using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLines;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.SetUpProductionLine;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines.Rules;
using SatisfactoryPlanner.Modules.Production.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Production.IntegrationTests.ProductionLines
{
    [TestFixture]
    public class SetUpProductionLineTests : TestBase
    {
        // Happy path tests
        [Test]
        public async Task WhenDataIsValid_IsSuccessful()
        {
            var worldId = Guid.NewGuid();

            var productionLineId = await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(worldId, "Rocky Desert Iron Ingots - Line 1"));

            var productionLines = await ProductionModule.ExecuteQueryAsync(new GetProductionLinesQuery(worldId));
            productionLines.Should().HaveCount(1);

            productionLines[0].Id.Should().Be(productionLineId);
            productionLines[0].Name.Should().Be("Rocky Desert Iron Ingots - Line 1");
        }

        [Test]
        public async Task CanAddSameProductionLineToDifferentWorlds()
        {
            const string ProductionLineName = "Rocky Desert Iron Ingots - Line 1";

            var world1Id = Guid.NewGuid();
            await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(world1Id, ProductionLineName));

            var world2Id = Guid.NewGuid();
            await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(world2Id, ProductionLineName));

            (await ProductionModule.ExecuteQueryAsync(new GetProductionLinesQuery(world1Id))).Count.Should().Be(1);
            (await ProductionModule.ExecuteQueryAsync(new GetProductionLinesQuery(world2Id))).Count.Should().Be(1);
        }

        // CommandValidator tests
        [Test]
        public void WhenWorldIdIsEmpty_ThrowsInvalidCommandException()
        {
            AssertInvalidCommand(async () => await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(Guid.Empty, "")));
        }

        // Command tests
        [Test]
        public async Task WhenProductionLineWithGivenNameAlreadyExists_BreaksProductionLineNameMustBeUniqueRule()
        {
            var worldId = Guid.NewGuid();

            await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(worldId, "Rocky Desert Iron Ingots - Line 1"));

            AssertBrokenRule<ProductionLineNameMustBeUniqueRule>(async () => await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(worldId, "ROCKY DESERT IRON INGOTS - LINE 1")));
        }
    }
}
