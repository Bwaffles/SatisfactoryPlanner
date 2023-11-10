using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Extractors
{
    public class Extractor : Entity, IAggregateRoot
    {
        private readonly List<AllowedResource> _allowedResources;
        private readonly ExtractorClockspeed _clockspeed;
        private readonly ExtractorCycle _cycle;

        public ExtractorId Id { get; }

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

        // ReSharper disable once UnusedMember.Local
        private Extractor()
        { /* for EF */
            _allowedResources = default!;
            _clockspeed = default!;
            _cycle = default!;
            Id = default!;
        }

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

        public decimal GetPotentialResourcesPerMinute()
        {
            return _cycle.GetResourcesPerMinute() * _clockspeed.GetMaxPotentialMultiplier();
        }

        internal bool CanExtract(ResourceId resourceId) => _allowedResources.Any(_ => _.Is(resourceId));
    }
}