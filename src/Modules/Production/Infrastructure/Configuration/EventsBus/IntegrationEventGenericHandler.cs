﻿using Autofac;
using Dapper;
using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Serialization;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.EventsBus
{
    internal class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T>
        where T : IntegrationEvent
    {
        public async Task Handle(T @event)
        {
            using var scope = ProductionCompositionRoot.BeginLifetimeScope();
            using var connection = scope.Resolve<IDbConnectionFactory>().GetOpenConnection();
            var type = @event.GetType().FullName;
            var data = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
            {
                ContractResolver = new AllPropertiesContractResolver()
            });

            const string sql = "INSERT INTO production.inbox_messages (id, occurred_on, type, data) " +
                               $"VALUES (@{nameof(@event.Id)}, @{nameof(@event.OccurredOn)}, @{nameof(type)}, @{nameof(data)})";
            var param = new
            {
                @event.Id, @event.OccurredOn, type, data
            };
            await connection.ExecuteScalarAsync(sql, param);
        }
    }
}