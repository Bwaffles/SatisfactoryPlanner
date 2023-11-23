using SatisfactoryPlanner.BuildingBlocks.Application;

namespace SatisfactoryPlanner.BuildingBlocks.IntegrationTests
{
    public class ExecutionContextMock(Guid userId) : IExecutionContextAccessor
    {
        public Guid UserId { get; set; } = userId;

        public Guid CorrelationId => throw new InvalidOperationException();

        public bool IsAvailable => throw new InvalidOperationException();
    }
}