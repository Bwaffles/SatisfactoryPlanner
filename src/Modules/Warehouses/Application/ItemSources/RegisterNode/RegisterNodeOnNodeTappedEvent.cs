using MediatR;
using SatisfactoryPlanner.Modules.Resources.IntegrationEvents;
using SatisfactoryPlanner.Modules.Warehouses.Application.Configuration;

namespace SatisfactoryPlanner.Modules.Warehouses.Application.ItemSources.RegisterNode
{
    public class RegisterNodeOnNodeTappedEvent(ICommandsScheduler commandsScheduler) : INotificationHandler<NodeTappedIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler = commandsScheduler;

        public async Task Handle(NodeTappedIntegrationEvent notification, CancellationToken cancellationToken)
            => await _commandsScheduler.EnqueueAsync(new RegisterNodeCommand(
                Guid.NewGuid(),
                notification.WorldId,
                notification.NodeId,
                notification.NodeName,
                notification.ItemId
                ));
    }
}
