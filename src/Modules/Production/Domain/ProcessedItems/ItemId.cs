using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems
{
    public record ItemId : TypedIdValueBase
    {
        public ItemId(Guid value) : base(value) { }
    }
}