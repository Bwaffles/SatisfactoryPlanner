using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.GameData.GameData;
using SatisfactoryPlanner.Modules.Warehouses.Application.Configuration;
using static SatisfactoryPlanner.Modules.Warehouses.Application.Items.GetItemStats.ItemStatsResult;

namespace SatisfactoryPlanner.Modules.Warehouses.Application.Items.GetItemStats
{
    public record GetItemStatsQuery(Guid WorldId) : QueryBase<ItemStatsResult>;

    internal class GetItemStatsQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetItemStatsQuery, ItemStatsResult>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<ItemStatsResult> Handle(GetItemStatsQuery request, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            const string sql = $"SELECT item_source.source_name as {nameof(ItemSourceDto.SourceName)}, " +
                               $"       produced_item.item_id as {nameof(ItemSourceDto.ProducedItemItemId)}, " +
                               $"       produced_item.rate as {nameof(ItemSourceDto.ProducedItemRate)} " +
                               "   FROM warehouses.item_sources as item_source " +
                               "   JOIN warehouses.produced_items as produced_item on produced_item.item_source_id = item_source.id " +
                               "  WHERE item_source.world_id = @WorldId;";


            var producedItemSources = (await connection.QueryAsync<ItemSourceDto>(sql, new
            {
                request.WorldId
            }))
            .ToList();

            var result = new ItemStatsResult
            {
                Items = []
            };

            foreach (var producedItemSource in producedItemSources)
            {
                var warehouseItem = result.Items.SingleOrDefault(item => item.ItemId == producedItemSource.ProducedItemItemId);
                if (warehouseItem == null)
                {
                    warehouseItem = new WarehouseItem
                    {
                        ItemId = producedItemSource.ProducedItemItemId,
                        ItemName = Item.GetById(producedItemSource.ProducedItemItemId).Name,
                        AmountProduced = 0,
                        AmountExported = 0,
                        AmountAvailable = 0,
                        AmountConsumed = 0,
                        AmountImported = 0,
                        ProducedAt = [],
                        ConsumedAt = []
                    };

                    result.Items.Add(warehouseItem);
                }

                var productionSource = new ProductionSource
                {
                    Name = producedItemSource.SourceName,
                    AmountProduced = producedItemSource.ProducedItemRate,
                    AmountExported = 0,
                    AmountAvailable = producedItemSource.ProducedItemRate,
                };

                warehouseItem.ProducedAt.Add(productionSource);

                warehouseItem.AmountProduced += producedItemSource.ProducedItemRate;
                warehouseItem.AmountAvailable = warehouseItem.AmountProduced - warehouseItem.AmountExported;
            }

            return result;
        }

        private class ItemSourceDto
        {
            public required string SourceName { get; set; }
            public required string ProducedItemItemId { get; set; }
            public decimal ProducedItemRate { get; set; }
        }
    }
}
