using SatisfactoryPlanner.Modules.Warehouses.Application.Configuration;
using static SatisfactoryPlanner.Modules.Warehouses.Application.Items.GetItemStats.ItemStatsResult;

namespace SatisfactoryPlanner.Modules.Warehouses.Application.Items.GetItemStats
{
    public record GetItemStatsQuery(Guid WorldId) : QueryBase<ItemStatsResult>;

    internal class GetItemStatsQueryHandler : IQueryHandler<GetItemStatsQuery, ItemStatsResult>
    {
        public Task<ItemStatsResult> Handle(GetItemStatsQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
                // TODO placeholder until we get the data loaded
                DateTime.Now.Minute % 2 == 0
                ? new ItemStatsResult
                {
                    Items = [
                    new WarehouseItem
                    {
                        ItemId = "IronOre",
                        ItemName = "Iron Ore",
                        AmountProduced = 300,
                        AmountExported = 300,
                        AmountAvailable = 0,
                        AmountConsumed = 300,
                        AmountImported = 300,
                        ProducedAt = [
                            new ProductionSource
                            {
                                Name = "Blue Crater 1",
                                AmountProduced = 300,
                                AmountExported = 300,
                                AmountAvailable = 0,
                            }
                        ],
                        ConsumedAt = [
                            new ConsumptionSource {
                                Name="Iron Ingot Factory Line 1",
                                AmountConsumed = 150,
                                AmountImported = 150
                            },
                            new ConsumptionSource {
                                Name="Iron Ingot Factory Line 2",
                                AmountConsumed = 150,
                                AmountImported = 150
                            }
                        ]
                    },
                    new WarehouseItem
                    {
                        ItemId = "IronIngot",
                        ItemName = "Iron Ingot",
                        AmountProduced = 300,
                        AmountExported = 310,
                        AmountAvailable = -10,
                        AmountConsumed = 300,
                        AmountImported = 300,
                        ProducedAt = [
                            new ProductionSource
                            {
                                Name = "Iron Ingot Factory Line 1",
                                AmountProduced = 150,
                                AmountExported = 150,
                                AmountAvailable = 0,
                            },
                            new ProductionSource
                            {
                                Name = "Iron Ingot Factory Line 2",
                                AmountProduced = 150,
                                AmountExported = 160,
                                AmountAvailable = -10,
                            }
                        ],
                        ConsumedAt = [
                            new ConsumptionSource {
                                Name="Iron Plate Line 1",
                                AmountConsumed = 300,
                                AmountImported = 300
                            }
                        ]
                    },
                    new WarehouseItem
                    {
                        ItemId = "IronPlates",
                        ItemName = "Iron Plates",
                        AmountProduced = 200,
                        AmountExported = 0,
                        AmountAvailable = 200,
                        AmountConsumed = 0,
                        AmountImported = 0,
                        ProducedAt = [
                            new ProductionSource
                            {
                                Name = "Iron Plate Line 1",
                                AmountProduced = 200,
                                AmountExported = 0,
                                AmountAvailable = 200,
                            }
                        ],
                        ConsumedAt = [
                        ]
                    }
                ]
                }
                : new ItemStatsResult { Items = [] }
            );
        }
    }
}
