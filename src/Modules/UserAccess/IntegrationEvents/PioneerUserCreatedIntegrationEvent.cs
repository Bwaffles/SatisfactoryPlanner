using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;

namespace SatisfactoryPlanner.Modules.UserAccess.IntegrationEvents
{
    public class PioneerUserCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }

        public PioneerUserCreatedIntegrationEvent(Guid id, DateTime occurredOn, Guid userId)
            : base(id, occurredOn) =>
            UserId = userId;
    }
}