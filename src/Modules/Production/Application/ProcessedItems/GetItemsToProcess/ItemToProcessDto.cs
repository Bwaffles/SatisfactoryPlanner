namespace SatisfactoryPlanner.Modules.Production.Application.ProcessedItems.GetItemsToProcess
{
    public class ItemToProcessDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ItemCategoryDto Category { get; set; }
    }

    public class ItemCategoryDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}