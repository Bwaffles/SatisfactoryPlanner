using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.Modules.GameData.GameData;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;
using SatisfactoryPlanner.Modules.Resources.IntegrationEvents;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.IncreaseExtractionRate;

public class PublishExtractionRateIncreasedIntegrationEvent(IEventsBus eventsBus, IDbConnectionFactory dbConnectionFactory) : IDomainEventNotificationHandler<ExtractionRateIncreasedNotification, ExtractionRateIncreasedDomainEvent>
{
    private readonly IEventsBus _eventsBus = eventsBus;
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task Handle(ExtractionRateIncreasedNotification notification, CancellationToken cancellationToken)
    {
        var connection = _dbConnectionFactory.GetOpenConnection();

        const string nodeDetailsSql =
            "     SELECT " +
            $"           world_node.world_id AS {nameof(WorldNodeDto.WorldId)}, " +
            $"           world_node.node_id AS {nameof(WorldNodeDto.NodeId)}, " +
            $"           world_node.resource_name AS {nameof(WorldNodeDto.ResourceName)} " +
            "       FROM resources.v_world_nodes AS world_node " +
            "      WHERE world_node.id = @WorldNodeId";

        var worldNode = await connection.QuerySingleAsync<WorldNodeDto>(nodeDetailsSql, new
        {
            WorldNodeId = notification.DomainEvent.WorldNodeId.Value
        });

        // The resources module duplicates the resource items and I'd like to move towards using the game data Item instead.
        // We create a unique resource id as a guid so it can't map to the Item.Id I created. I'd like to move towards that,
        // but in the meantime the only way to map them is with the name.
        var item = Item.All.Single(item => item.Name == worldNode.ResourceName);

        await _eventsBus.Publish(new ExtractionRateIncreasedIntegrationEvent(
            notification.DomainEvent.Id,
            notification.DomainEvent.OccurredOn,
            worldNode.WorldId,
            worldNode.NodeId,
            item.Id,
            notification.DomainEvent.ExtractionRate
        ));
    }
    private class WorldNodeDto
    {
        public required Guid WorldId { get; set; }
        public required Guid NodeId { get; set; }
        public required string ResourceName { get; set; }
    }
}
