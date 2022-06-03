using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations.Events
{
    public class UserRegistrationConfirmedDomainEvent : DomainEventBase
    {
        public UserRegistrationId UserRegistrationId { get; }

        public UserRegistrationConfirmedDomainEvent(UserRegistrationId userRegistrationId) =>
            UserRegistrationId = userRegistrationId;
    }
}