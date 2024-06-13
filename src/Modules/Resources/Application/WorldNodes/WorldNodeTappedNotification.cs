using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes
{
    [method: JsonConstructor]
    public class WorldNodeTappedNotification(WorldNodeTappedDomainEvent domainEvent) : DomainEventNotification<WorldNodeTappedDomainEvent>(domainEvent);
}
