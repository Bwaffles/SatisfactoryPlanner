using Dapper;
using MediatR;
using Newtonsoft.Json;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Processing.Outbox;
using System.Data;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork
{
    public static class OutboxMessagesHelper
    {
        public static async Task<List<OutboxMessageDto>> GetOutboxMessages(IDbConnection connection)
        {
            const string sql = "  SELECT outbox_message.id, " +
                               "         outbox_message.type, " +
                               "         outbox_message.data " +
                               "    FROM resources.outbox_messages AS outbox_message " +
                               "ORDER BY outbox_message.occurred_on";

            return (await connection.QueryAsync<OutboxMessageDto>(sql)).AsList();
        }

        public static T Deserialize<T>(OutboxMessageDto message)
            where T : class, INotification
        {
            var typeName = typeof(T).FullName;
            if (typeName == null)
                throw new InvalidOperationException();

            var type = Assembly.GetAssembly(typeof(WorldNodeTappedNotification))!.GetType(typeName)!;
            return JsonConvert.DeserializeObject(message.Data, type) as T ?? throw new InvalidOperationException();
        }
    }
}