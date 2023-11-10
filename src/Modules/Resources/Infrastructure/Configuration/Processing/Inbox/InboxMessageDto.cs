using System;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Processing.Inbox
{
    public class InboxMessageDto
    {
        public Guid Id { get; set; }

        public required string Type { get; set; }

        public required string Data { get; set; }
    }
}