﻿using SatisfactoryPlanner.BuildingBlocks.Application;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork
{
    public class ExecutionContextMock : IExecutionContextAccessor
    {
        public ExecutionContextMock(Guid userId) => UserId = userId;

        public Guid UserId { get; }

        public Guid CorrelationId { get; }

        public bool IsAvailable { get; }
    }
}