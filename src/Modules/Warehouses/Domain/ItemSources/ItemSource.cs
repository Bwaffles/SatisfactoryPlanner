using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.GameData.GameData;
using SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources.Events;

namespace SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources
{
    /// <summary>
    /// A source for items. A source can produce and consume 0 or more items.
    ///
    /// <para>
    /// A node source would have 1 produced item and 0 consumed items. The produced item can be exported to 0 or more sources.
    /// This gives us the ability to say we are using 300/600 of the produced ore, or that we have 300 ore remaining to be processed.
    /// </para>
    ///
    /// <para>
    /// A processed item from a production line can have 1 or more produced and consumed items. A source creating quickwire for example,
    /// would consume both Copper Ingot and Caterium Ingot, and produce Quickwire. They can inport both the copper and caterium ingots
    /// from other processed items, and export the quickwire to another processed item for consumption. We can then say that
    /// this Quickwire production utilizied 100/200 copper ingots, 200/200 caterium ingots, and 400/400 quickwire. We could see
    /// that we're missing some of the items and that the quickwire production will be affected.
    /// </para>
    /// </summary>
    public sealed class ItemSource : Entity, IAggregateRoot
    {
        public ItemSourceId Id { get; }
        private readonly WorldId _worldId;
        private readonly Source _source;

        /// <summary>
        /// Items that are produced at this location. This can either be a node resource extraction, or a production line producing new items.
        /// </summary>
        private readonly List<ProducedItem> _producedItems;

        ///// <summary>
        ///// Items that are consumed at this location to create the produced items.
        ///// </summary>
        //private readonly List<ConsumedItem> _consumedItems;

        private ItemSource()
        { /* Only for EF. */
            Id = default!;
            _worldId = default!;
            _source = default!;
            _producedItems = default!;
            //_consumedItems = default!;
        }

        private ItemSource(WorldId worldId, Source source)
        {
            Id = new ItemSourceId(Guid.NewGuid());
            _worldId = worldId;
            _source = source;
            _producedItems = [];
            //_consumedItems = [];

            AddDomainEvent(new ItemSourceRegisteredDomainEvent(Id, _worldId, source));
        }

        public static ItemSource Register(WorldId worldId, Source source) => new(worldId, source);

        ///// <summary>
        ///// Set up the amount of items that the source consumes.
        ///// </summary>
        //internal void Consumes(decimal amount, string itemId)
        //{
        //    _consumedItems.Add(new ConsumedItem(itemId, amount));
        //}

        /// <summary>
        /// Set up the rate of items that the source produces.
        /// </summary>
        public void Produces(Item item, Rate rate)
        {
            _producedItems.Add(ProducedItem.CreateNew(Id, item, rate));
        }

        public void ChangeProductionRate(Item item, Rate rate)
        {
            var producedItem = GetProducedItem(item);
            producedItem.ChangeRate(rate);
        }

        ///// <summary>
        ///// Import <paramref name="amount"/> of consumed <paramref name="itemId"/> from the <paramref name="fromSource"/>.
        ///// </summary>
        //internal void Import(decimal amount, string itemId, ItemSource fromSource)
        //{
        //    var consumedItem = FindConsumedItem(itemId);
        //    consumedItem.AddImport(new Import(amount, fromSource));
        //}

        ///// <summary>
        ///// Export <paramref name="amount"/> of produced <paramref name="itemId"/> to the <paramref name="toSource"/>.
        ///// </summary>
        //internal void Export(decimal amount, string itemId, ItemSource toSource)
        //{
        //    var producedItem = FindProducedItem(itemId);
        //    producedItem.AddExport(new Export(amount, toSource));
        //}

        //private ConsumedItem FindConsumedItem(string itemId)
        //{
        //    return _consumedItems.Find(item => item.ItemId == itemId);
        //}

        private ProducedItem GetProducedItem(Item item) => _producedItems.Single(producedItem => producedItem.ItemId == item.Id);
    }
}
