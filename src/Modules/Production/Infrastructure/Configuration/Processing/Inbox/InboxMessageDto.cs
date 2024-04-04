using System;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Processing.Inbox
{
    public class InboxMessageDto
    {
        public Guid Id { get; set; }

        public required string Type { get; set; }

        public required string Data { get; set; }
    }
}