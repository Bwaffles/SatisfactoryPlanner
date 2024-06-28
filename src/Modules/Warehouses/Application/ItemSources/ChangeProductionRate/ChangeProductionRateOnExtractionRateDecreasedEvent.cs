using MediatR;
using SatisfactoryPlanner.Modules.Resources.IntegrationEvents;
using SatisfactoryPlanner.Modules.Warehouses.Application.Configuration;

namespace SatisfactoryPlanner.Modules.Warehouses.Application.ItemSources.ChangeProductionRate;

public class ChangeProductionRateOnExtractionRateDecreasedEvent(ICommandsScheduler commandsScheduler) : INotificationHandler<ExtractionRateDecreasedIntegrationEvent>
{
    private readonly ICommandsScheduler _commandsScheduler = commandsScheduler;

    public async Task Handle(ExtractionRateDecreasedIntegrationEvent notification, CancellationToken cancellationToken)
        => await _commandsScheduler.EnqueueAsync(new ChangeProductionRateCommand(
            Guid.NewGuid(),
            notification.WorldId,
            notification.NodeId,
            notification.ItemId,
            notification.Rate
            ));
}
