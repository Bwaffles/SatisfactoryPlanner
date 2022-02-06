namespace Application.ProductionLines.Queries.Export
{
    public class ExportItemModel
    {
        public string Name { get; internal set; }

        public decimal Amount { get; internal set; }

        /// <summary>
        /// The unique id of the item.
        /// </summary>
        public int Id { get; internal set; }
        public string ItemId { get; internal set; }
    }
}
