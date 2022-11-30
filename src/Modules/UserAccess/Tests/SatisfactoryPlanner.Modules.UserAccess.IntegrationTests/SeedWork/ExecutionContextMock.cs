using SatisfactoryPlanner.BuildingBlocks.Application;

namespace SatisfactoryPlanner.Modules.UserAccess.IntegrationTests.SeedWork
{
    public class ExecutionContextMock : IExecutionContextAccessor
    {
        public ExecutionContextMock(Guid userId) => UserId = userId;

        public Guid UserId { get; set; }

        public Guid CorrelationId { get; }

        public bool IsAvailable { get; }
    }
}