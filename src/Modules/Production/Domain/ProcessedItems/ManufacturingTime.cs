using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems
{
    public class ManufacturingTime : ValueObject
    {
        /// <summary>
        /// The number of seconds it takes to produce the items.
        /// </summary>
        public decimal Duration { get; }

        /// <summary>
        /// When producing an item manually at the craft bench or workshop, this value is multiplied by the Duration
        /// to determine how long it will take to manually craft the item in seconds.
        /// </summary>
        public decimal ManualMultiplier { get; }

        private ManufacturingTime(decimal duration, decimal manualMultiplier)
        {
            Duration = duration;
            ManualMultiplier = manualMultiplier;
        }

        public static ManufacturingTime Of(decimal duration, decimal manualMultiplier)
            => new(duration, manualMultiplier);
    }
}