using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Extractors
{
    public class Extractor : Entity, IAggregateRoot
    {
        public ExtractorId Id { get; }

        private readonly ExtractorCycle _cycle;

        private readonly ExtractorClockspeed _clockspeed;

        private readonly List<AllowedResource> _allowedResources;

        public static Extractor CreateNew(
            ExtractorId id,
            ExtractorCycle cycle,
            ExtractorClockspeed clockspeed,
            List<ResourceId> allowedResources)
            => new(
                id,
                cycle,
                clockspeed,
                allowedResources.Select(_ => AllowedResource.CreateNew(id, _)).ToList());

        private Extractor(
            ExtractorId id,
            ExtractorCycle cycle,
            ExtractorClockspeed clockspeed,
            List<AllowedResource> allowedResources)
        {
            Id = id;
            _cycle = cycle;
            _clockspeed = clockspeed;
            _allowedResources = allowedResources;
        }

        private Extractor() { }

        public decimal GetPotentialResourcesPerMinute()
        {
            return _cycle.GetResourcesPerMinute() * _clockspeed.GetMaxPotentialMultiplier();
        }

        internal bool CanExtract(ResourceId resourceId) => _allowedResources.Any(_ => _.Is(resourceId));
    }
}
