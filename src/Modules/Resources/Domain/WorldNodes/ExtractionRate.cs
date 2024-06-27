using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes
{
    public sealed class ExtractionRate : ValueObject
    {
        /// <summary>
        ///     The rate of extraction in resources per minute.
        /// </summary>
        public decimal Rate { get; }

        private ExtractionRate(decimal rate)
        {
            if (rate < 0)
                throw new ArgumentOutOfRangeException(nameof(rate), rate, "Rate cannot be negative.");

            Rate = rate;
        }

        public static ExtractionRate Of(decimal rate) => new(rate);

        public static bool operator >(ExtractionRate left, ExtractionRate right) => left.Rate > right.Rate;

        public static bool operator <(ExtractionRate left, ExtractionRate right) => left.Rate < right.Rate;
    }
}