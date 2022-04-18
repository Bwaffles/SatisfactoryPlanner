using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests
{
    internal class ExtractorFixture
    {
        private readonly List<ResourceId> _allowedResources = new List<ResourceId>();

        public Extractor Build()
        {
            return Extractor.CreateNew(
                new ExtractorId(Guid.NewGuid()),
                ExtractorCycle.CreateNew(1, 1),
                ExtractorClockspeed.CreateNew(1, 0.5m, 3),
                _allowedResources);
        }

        internal ExtractorFixture CanExtract(ResourceId resourceId)
        {
            _allowedResources.Add(resourceId);
            return this;
        }

        internal ExtractorFixture CannotExtract(ResourceId resourceId)
        {
            _allowedResources.Remove(resourceId);
            return this;
        }
    }
}
