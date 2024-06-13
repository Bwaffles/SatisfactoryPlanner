using MediatR;
using SatisfactoryPlanner.Modules.Resources.IntegrationEvents;
using SatisfactoryPlanner.Modules.Warehouses.Application.Configuration;

namespace SatisfactoryPlanner.Modules.Warehouses.Application.ItemSources.CreateItemSource
{
    public class CreateItemSourceOnWorldNodeTappedEvent(ICommandsScheduler commandsScheduler) : INotificationHandler<WorldNodeTappedIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler = commandsScheduler;

        public async Task Handle(WorldNodeTappedIntegrationEvent notification, CancellationToken cancellationToken)
            => await _commandsScheduler.EnqueueAsync(new CreateItemSourceCommand());
    }
}
