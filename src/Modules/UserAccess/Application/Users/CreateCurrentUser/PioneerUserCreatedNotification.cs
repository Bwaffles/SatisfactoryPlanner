using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users.Events;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Users.CreateCurrentUser
{
    public class PioneerUserCreatedNotification : DomainEventNotificationBase<PioneerUserCreatedDomainEvent>
    {
        [JsonConstructor]
        public PioneerUserCreatedNotification(PioneerUserCreatedDomainEvent domainEvent, Guid id)
            : base(domainEvent, id) { }
    }
}