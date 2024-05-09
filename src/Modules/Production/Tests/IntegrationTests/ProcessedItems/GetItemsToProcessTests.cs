using SatisfactoryPlanner.Modules.Production.Application.ProcessedItems.GetItemsToProcess;
using SatisfactoryPlanner.Modules.Production.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Production.IntegrationTests.ProcessedItems
{
    [TestFixture]
    public class GetItemsToProcessTests : IntegrationTest
    {
        // Happy path tests
        [Test]
        public async Task CanGetItemsToProcess()
        {
            var itemsToProcess = await ProductionModule.ExecuteQueryAsync(new GetItemsToProcessQuery());

            AssertAll(() =>
            {
                itemsToProcess.Should().NotBeNullOrEmpty();
                itemsToProcess.Should().OnlyHaveUniqueItems();

                // Pick an item guaranteed to be returned and test properties are set as expected
                itemsToProcess.Should().ContainEquivalentOf(
                    new ItemToProcessDto()
                    {
                        Id = "IronOre",
                        Name = "Iron Ore",
                        Category = new ItemCategoryDto()
                        {
                            Id = "Resources",
                            Name = "Resources"
                        }
                    });
            });
        }
    }
}
