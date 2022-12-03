using MediatR;
using SatisfactoryPlanner.Modules.Pioneers.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.UserAccess.IntegrationEvents;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.SpawnPioneer
{
    public class PioneerUserCreatedIntegrationEventHandler : INotificationHandler<PioneerUserCreatedIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public PioneerUserCreatedIntegrationEventHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }
        public async Task Handle(PioneerUserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new SpawnPioneerCommand(Guid.NewGuid(), notification.UserId));
        }
            
    }
}