using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Extractors
{
    public class ExtractorId : TypedIdValueBase
    {
        public ExtractorId(Guid value) : base(value) { }
    }
}