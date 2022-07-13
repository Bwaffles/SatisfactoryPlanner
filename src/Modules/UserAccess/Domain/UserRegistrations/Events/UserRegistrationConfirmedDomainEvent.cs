using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations.Events
{
    public class UserRegistrationConfirmedDomainEvent : DomainEventBase
    {
        public UserRegistrationId UserRegistrationId { get; }

        public DateTime ConfirmedDate { get; }

        public UserRegistrationConfirmedDomainEvent(UserRegistrationId userRegistrationId, DateTime confirmedDate)
        {
            UserRegistrationId = userRegistrationId;
            ConfirmedDate = confirmedDate;
        }
    }
}