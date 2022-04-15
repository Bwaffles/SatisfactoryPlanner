using SatisfactoryPlanner.BuildingBlocks.Domain;
using System.Collections.Generic;

namespace SatisfactoryPlanner.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public interface IDomainEventsAccessor
    {
        IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}