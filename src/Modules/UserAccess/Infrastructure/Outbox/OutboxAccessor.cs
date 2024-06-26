﻿using SatisfactoryPlanner.BuildingBlocks.Application.Outbox;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Outbox
{
    public class OutboxAccessor : IOutbox
    {
        private readonly UserAccessContext _userAccessContext;

        public OutboxAccessor(UserAccessContext userAccessContext)
        {
            _userAccessContext = userAccessContext;
        }

        public void Add(OutboxMessage message)
        {
            _userAccessContext.OutboxMessages.Add(message);
        }

        public Task Save()
        {
            return
                Task.CompletedTask; // Save is done automatically using EF Core Change Tracking mechanism during SaveChanges.
        }
    }
}