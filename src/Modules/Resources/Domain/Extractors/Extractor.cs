using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Extractors
{
    public class Extractor : Entity, IAggregateRoot
    {
        public ExtractorId Id { get; }

        private readonly decimal _extractCycleTime;

        private readonly decimal _itemsPerCycle;

        private readonly decimal _maxClockspeed;

        private readonly decimal _maxClockspeedPerShard;

        private readonly int _maxShards;

        private readonly List<AllowedResource> _allowedResources;

        private Extractor() { }

        public decimal GetPotentialResourcesPerMinute()
        {
            const int secondsPerMinute = 60;
            var itemsPerSecond = secondsPerMinute / _extractCycleTime * _itemsPerCycle;
            var maxPotentialClockspeed = _maxClockspeed + _maxClockspeedPerShard * _maxShards;

            return itemsPerSecond * maxPotentialClockspeed;
        }

        internal bool CanExtract(ResourceId resourceId) => _allowedResources.Any(_ => _.Is(resourceId));
    }
}
