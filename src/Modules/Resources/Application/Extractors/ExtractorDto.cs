using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.Extractors
{
    public class ExtractorDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public decimal SecondsToCompleteCycle { get; set; }

        public decimal ResourcesExtractedPerCycle { get; set; }

        public decimal DefaultClockspeed { get; set; }

        public decimal OverclockPerShard { get; set; }

        public int MaxShards { get; set; }
    }
}
