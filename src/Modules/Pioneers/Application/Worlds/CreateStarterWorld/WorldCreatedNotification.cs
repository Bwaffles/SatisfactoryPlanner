using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.Modules.Worlds.Domain.Worlds.Events;

namespace SatisfactoryPlanner.Modules.Worlds.Application.Worlds.CreateStarterWorld
{
    public class WorldCreatedNotification : DomainNotificationBase<WorldCreatedDomainEvent>
    {
        [JsonConstructor]
        public WorldCreatedNotification(WorldCreatedDomainEvent domainEvent, Guid id) 
            : base(domainEvent, id) { }
    }
}