﻿using Dapper;
using MediatR;
using Newtonsoft.Json;
using SatisfactoryPlanner.Modules.Worlds.Application.Worlds.CreateStarterWorld;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.Processing.Outbox;
using System.Data;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Worlds.IntegrationTests.SeedWork
{
    public class OutboxMessagesHelper
    {
        public static async Task<List<OutboxMessageDto>> GetOutboxMessages(IDbConnection connection)
        {
            const string sql = "  SELECT outbox_message.id, " +
                               "         outbox_message.type, " +
                               "         outbox_message.data " +
                               "    FROM worlds.outbox_messages AS outbox_message " +
                               "ORDER BY outbox_message.occurred_on";

            return (await connection.QueryAsync<OutboxMessageDto>(sql)).AsList();
        }

        public static T Deserialize<T>(OutboxMessageDto message)
            where T : class, INotification
        {
            var typeName = typeof(T).FullName;
            if (typeName == null)
                throw new InvalidOperationException();

            var type = Assembly.GetAssembly(typeof(WorldCreatedNotification))!.GetType(typeName)!;
            return JsonConvert.DeserializeObject(message.Data, type) as T ?? throw new InvalidOperationException();
        }
    }
}