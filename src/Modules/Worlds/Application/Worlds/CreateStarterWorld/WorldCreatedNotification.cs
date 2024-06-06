using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.Modules.Worlds.Domain.Worlds.Events;

namespace SatisfactoryPlanner.Modules.Worlds.Application.Worlds.CreateStarterWorld
{
    [method: JsonConstructor]
    public class WorldCreatedNotification(WorldCreatedDomainEvent domainEvent) : DomainEventNotification<WorldCreatedDomainEvent>(domainEvent);
}