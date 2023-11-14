using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Extractors
{
    public class ExtractorClockspeed : ValueObject
    {
        /// <summary>
        ///     The highest the clockspeed can be set without the addition of power shards as a percentage e.g. 1.00 = 100%.
        /// </summary>
        public decimal DefaultClockspeed { get; }

        /// <summary>
        ///     How much the clockspeed increases above the <see cref="DefaultClockspeed" /> when a shard is placed in the
        ///     extractor
        ///     as a percentage, e.g. 0.5 = 50%.
        /// </summary>
        public decimal OverclockPerShard { get; }

        /// <summary>
        ///     How many shards can be placed in the extractor for overclocking.
        /// </summary>
        public int MaxShards { get; }

        private ExtractorClockspeed(decimal defaultClockspeed, decimal overclockPerShard, int maxShards)
        {
            DefaultClockspeed = defaultClockspeed;
            OverclockPerShard = overclockPerShard;
            MaxShards = maxShards;
        }

        public static ExtractorClockspeed CreateNew(decimal defaultClockspeed, decimal overclockPerShard, int maxShards)
            => new(defaultClockspeed, overclockPerShard, maxShards);

        internal decimal GetMaxPotentialMultiplier()
        {
            return DefaultClockspeed + (OverclockPerShard * MaxShards);
        }
    }
}