namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems
{
    /// <summary>
    /// The form this item is in, i.e. does it require pipes or conveyors, can the player pick it up etc.
    /// </summary>
    public enum ResourceForm
    {
        Unknown,
        Solid,
        Liquid,
        Gas
    }
}
