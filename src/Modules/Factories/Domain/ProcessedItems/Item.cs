using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems
{
    public class Item : ValueObject
    {
        private Item() { }

        public static Item As() => new();
    }
}