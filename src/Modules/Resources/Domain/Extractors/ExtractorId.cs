using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Extractors
{
    public record ExtractorId : TypedIdValueBase
    {
        public ExtractorId(Guid value) : base(value) { }
    }
}