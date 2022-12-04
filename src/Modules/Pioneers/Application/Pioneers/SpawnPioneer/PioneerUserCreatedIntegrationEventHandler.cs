using MediatR;
using SatisfactoryPlanner.Modules.UserAccess.IntegrationEvents;
using SatisfactoryPlanner.Modules.Worlds.Application.Configuration.Commands;

namespace SatisfactoryPlanner.Modules.Worlds.Application.Pioneers.SpawnPioneer
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