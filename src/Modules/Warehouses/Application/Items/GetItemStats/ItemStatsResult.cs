namespace SatisfactoryPlanner.Modules.Warehouses.Application.Items.GetItemStats;

public class ItemStatsResult
{
    public required List<WarehouseItem> Items { get; init; }

    public class WarehouseItem
    {
        public required string ItemId { get; init; }
        public required string ItemName { get; init; }
        public required decimal AmountProduced { get; set; }
        public required decimal AmountExported { get; set; }
        public required decimal AmountAvailable { get; set; }
        public required decimal AmountConsumed { get; set; }
        public required decimal AmountImported { get; set; }
        public required List<ProductionSource> ProducedAt { get; init; }
        public required List<ConsumptionSource> ConsumedAt { get; init; }
    }

    public class ProductionSource
    {
        public required string Name { get; init; }
        public required decimal AmountProduced { get; init; }
        public required decimal AmountExported { get; init; }
        public required decimal AmountAvailable { get; init; }
    }

    public class ConsumptionSource
    {
        public required string Name { get; init; }
        public required decimal AmountConsumed { get; init; }
        public required decimal AmountImported { get; init; }
    }
}
