using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.Extractors.GetExtractors
{
    public class ExtractorDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal SecondsToCompleteCycle { get; set; }

        public decimal ResourcesExtractedPerCycle { get; set; }

        public decimal DefaultClockspeed { get; set; }

        public decimal OverclockPerShard { get; set; }

        public int MaxShards { get; set; }
    }
}
