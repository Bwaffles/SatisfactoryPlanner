using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using System;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests
{
    internal class ExtractorFixture
    {
        private readonly List<ResourceId> _allowedResources = new List<ResourceId>();
        private string _type = "Miner";

        public Extractor Build()
        {
            return Extractor.CreateNew(
                new ExtractorId(Guid.NewGuid()),
                GetExtractorCycle(),
                ExtractorClockspeed.CreateNew(1, 0.5m, 3),
                _allowedResources);
        }

        private ExtractorCycle GetExtractorCycle()
        {
            switch (_type)
            {
                case "Miner": return ExtractorCycle.CreateNew(0.25m, 1); // Mk. 3
                case "OilExtractor": return ExtractorCycle.CreateNew(1, 2000);
                default: throw new InvalidOperationException();
            }
        }

        internal ExtractorFixture AsOilExtractor()
        {
            _type = "OilExtractor";
            return this;
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
