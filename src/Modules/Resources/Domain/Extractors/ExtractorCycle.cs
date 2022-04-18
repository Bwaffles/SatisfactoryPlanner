using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Extractors
{
    public class ExtractorCycle : ValueObject
    {
        /// <summary>
        ///     How long the cycle takes to complete in seconds.
        /// </summary>
        public decimal SecondsToComplete { get; }

        /// <summary>
        ///     How many resources are extracted per cycle.
        /// </summary>
        public decimal ResourcesExtracted { get; }

        public static ExtractorCycle CreateNew(decimal secondsToComplete, decimal resourcesExtracted)
            => new(secondsToComplete, resourcesExtracted);

        private ExtractorCycle(decimal secondsToComplete, decimal resourcesExtracted)
        {
            SecondsToComplete = secondsToComplete;
            ResourcesExtracted = resourcesExtracted;
        }

        internal decimal GetResourcesPerMinute()
        {
            const int secondsPerMinute = 60;
            return (secondsPerMinute / SecondsToComplete) * ResourcesExtracted;
        }
    }
}
