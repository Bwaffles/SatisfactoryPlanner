using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems
{
    public record ProcessedItemId : TypedIdValueBase
    {
        public ProcessedItemId(Guid value) : base(value) { }
    }
}