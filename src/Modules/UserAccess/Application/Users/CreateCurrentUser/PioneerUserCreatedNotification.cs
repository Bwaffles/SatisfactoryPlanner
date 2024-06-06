using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users.Events;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Users.CreateCurrentUser
{
    [method: JsonConstructor]
    public class PioneerUserCreatedNotification(PioneerUserCreatedDomainEvent domainEvent) : DomainEventNotification<PioneerUserCreatedDomainEvent>(domainEvent);
}