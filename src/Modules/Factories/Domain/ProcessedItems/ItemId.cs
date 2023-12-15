using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems
{
    public record ItemId : TypedIdValueBase
    {
        public ItemId(Guid value) : base(value) { }
    }
}