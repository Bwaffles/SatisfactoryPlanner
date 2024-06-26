using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.GameData.GameData;
using SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources.Events;

namespace SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources
{
    public sealed class ProducedItem : Entity
    {
        public ItemSourceId ItemSourceId { get; }
        public string ItemId { get; }

        private readonly Rate _rate;
        //private readonly List<Export> _exports;

        private ProducedItem()
        {
            ItemSourceId = default!;
            ItemId = default!;
            _rate = default!;
        }

        private ProducedItem(ItemSourceId itemSourceId, string itemId, Rate rate)
        {
            ItemSourceId = itemSourceId;
            ItemId = itemId;
            _rate = rate;
            //_exports = [];

            AddDomainEvent(new ItemProducedDomainEvent(ItemSourceId, ItemId, _rate));
        }

        public static ProducedItem CreateNew(ItemSourceId itemSourceId, Item item, Rate rate) => new(itemSourceId, item.Id, rate);

        //internal void AddExport(Export export)
        //{
        //    _exports.Add(export);
        //}
    }
}
