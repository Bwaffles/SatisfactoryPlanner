using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLines;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.SetUpProductionLine;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines.Rules;
using SatisfactoryPlanner.Modules.Production.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Production.IntegrationTests.ProductionLines
{
    [TestFixture]
    public class RenameProductionLineTests : TestBase
    {
        // Happy path tests
        [Test]
        public async Task WhenDataIsValid_IsSuccessful()
        {
            var worldId = Guid.NewGuid();
            var productionLineId = await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(worldId, "Rocky Desert Iron Ingots - Line 1"));

            await ProductionModule.ExecuteCommandAsync(new RenameProductionLineCommand(worldId, productionLineId, "Rocky Desert Iron Ingots - Line 2"));

            var productionLine = (await ProductionModule.ExecuteQueryAsync(new GetProductionLineDetailsQuery(worldId, productionLineId)))!;
            productionLine.Name.Should().Be("Rocky Desert Iron Ingots - Line 2");
        }

        [Test]
        public async Task CanRenameProductionLineWhenNameNotUsedInThatWorld()
        {
            // Some other world has line 1 already
            await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(Guid.NewGuid(), "Rocky Desert Iron Ingots - Line 1"));

            // We add a new production line in our world, we should be able to rename it even though it exists in another world already
            var worldId = Guid.NewGuid();
            var productionLineId = await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(worldId, "Rocky Desert Iron Ingots - Line 2"));

            await ProductionModule.ExecuteCommandAsync(new RenameProductionLineCommand(worldId, productionLineId, "Rocky Desert Iron Ingots - Line 1"));

            var productionLine = (await ProductionModule.ExecuteQueryAsync(new GetProductionLineDetailsQuery(worldId, productionLineId)))!;
            productionLine.Name.Should().Be("Rocky Desert Iron Ingots - Line 1");
        }

        // CommandValidator tests
        [Test]
        public void WhenWorldIdIsEmpty_ThrowsInvalidCommandException()
        {
            AssertInvalidCommand(async () => await ProductionModule.ExecuteCommandAsync(new RenameProductionLineCommand(Guid.Empty, Guid.NewGuid(), "")));
        }

        [Test]
        public void WhenProductionLineIdIsEmpty_ThrowsInvalidCommandException()
        {
            AssertInvalidCommand(async () => await ProductionModule.ExecuteCommandAsync(new RenameProductionLineCommand(Guid.NewGuid(), Guid.Empty, "")));
        }

        // Command tests
        [Test]
        public async Task WhenProductionLineDoesNotExistInWorld_ThrowsInvalidCommandException()
        {
            var worldId = Guid.NewGuid();
            var productionLineId = await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(worldId, "Rocky Desert Iron Ingots - Line 1"));

            AssertInvalidCommand(async () => await ProductionModule.ExecuteCommandAsync(new RenameProductionLineCommand(Guid.NewGuid(), productionLineId, "Rocky Desert Iron Ingots - Line 2")));
        }

        [Test]
        public async Task WhenProductionLineWithGivenNameAlreadyExists_BreaksProductionLineNameMustBeUniqueRule()
        {
            var worldId = Guid.NewGuid();
            var productionLineId1 = await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(worldId, "Rocky Desert Iron Ingots - Line 1"));
            var productionLineId2 = await ProductionModule.ExecuteCommandAsync(new SetUpProductionLineCommand(worldId, "Rocky Desert Iron Ingots - Line 2"));

            AssertBrokenRule<ProductionLineNameMustBeUniqueRule>(async () 
                => await ProductionModule.ExecuteCommandAsync(new RenameProductionLineCommand(worldId, productionLineId2, "Rocky Desert Iron Ingots - Line 1")));
        }
    }
}
