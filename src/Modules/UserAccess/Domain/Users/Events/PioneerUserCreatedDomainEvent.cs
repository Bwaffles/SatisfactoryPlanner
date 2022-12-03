using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.Users.Events
{
    public class PioneerUserCreatedDomainEvent : DomainEventBase
    {
        public UserId UserId { get; }

        public PioneerUserCreatedDomainEvent(UserId userId) => UserId = userId;
    }
}