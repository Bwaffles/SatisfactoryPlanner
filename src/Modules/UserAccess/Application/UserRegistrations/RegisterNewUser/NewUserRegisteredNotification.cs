using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations.Events;
using System;
using System.Text.Json.Serialization;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser
{
    public class NewUserRegisteredNotification : DomainNotificationBase<NewUserRegisteredDomainEvent>
    {
        [JsonConstructor]
        public NewUserRegisteredNotification(NewUserRegisteredDomainEvent domainEvent, Guid id)
            : base(domainEvent, id)
        {
        }
    }
}
