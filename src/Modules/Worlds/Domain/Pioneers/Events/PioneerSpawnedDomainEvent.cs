﻿using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers.Events
{
    public class PioneerSpawnedDomainEvent : DomainEventBase
    {
        public PioneerId PioneerId { get; }

        public PioneerSpawnedDomainEvent(PioneerId pioneerId) => PioneerId = pioneerId;
    }
}