using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Factories.Domain.Resources;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ResourceExtractors
{
    public class ResourceExtractor : Entity, IAggregateRoot
    {
        public ResourceExtractorId Id { get; }

        private decimal _extractCycleTime;

        private decimal _itemsPerCycle;

        private decimal _maxClockspeed;

        private decimal _maxClockspeedPerShard;

        private int _maxShards;

        private List<ResourceExtractorAllowedResource> _allowedResources;

        private ResourceExtractor() { }

        public decimal GetPotentialItemsPerMinute()
        {
            const int secondsPerMinute = 60;
            var itemsPerSecond = (secondsPerMinute / _extractCycleTime) * _itemsPerCycle;
            var maxPotentialClockspeed = _maxClockspeed + (_maxClockspeedPerShard * _maxShards);

            return itemsPerSecond * maxPotentialClockspeed;
        }

        internal bool CanExtract(ResourceId resourceId) => _allowedResources.Any(_ => _.IsResource(resourceId));
    }
}
