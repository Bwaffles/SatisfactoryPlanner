﻿using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox
{
    public class OutboxMessageDto
    {
        public Guid Id { get; set; }

        public required string Type { get; set; }

        public required string Data { get; set; }
    }
}