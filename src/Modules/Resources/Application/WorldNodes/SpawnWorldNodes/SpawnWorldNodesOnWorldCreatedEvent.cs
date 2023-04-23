using MediatR;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Worlds.IntegrationEvents;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.SpawnWorldNodes
{
    // ReSharper disable once UnusedMember.Global
    public class SpawnWorldNodesOnWorldCreatedEvent : INotificationHandler<WorldCreatedIntegrationEvent>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public SpawnWorldNodesOnWorldCreatedEvent(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(WorldCreatedIntegrationEvent notification, CancellationToken cancellationToken)
            => await _commandsScheduler
                .EnqueueAsync(new SpawnWorldNodesCommand(Guid.NewGuid(), notification.WorldId));
    }
}