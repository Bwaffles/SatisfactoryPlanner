using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems
{
    public record ProcessedItemId : TypedIdValueBase
    {
        public ProcessedItemId(Guid value) : base(value) { }
    }
}