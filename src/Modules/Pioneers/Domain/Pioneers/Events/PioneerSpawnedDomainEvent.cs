using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers.Events
{
    public class PioneerSpawnedDomainEvent : DomainEventBase
    {
        public PioneerId PioneerId { get; }

        public PioneerSpawnedDomainEvent(PioneerId pioneerId) => PioneerId = pioneerId;
    }
}