using Dapper;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using System.Data;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork
{
    public static class DatabaseClearer
    {
        public static async Task Clear(IDbConnection connection)
        {
            /* Not clearing the following tables since they're pre-loaded reference tables:
             *   extractor_allowed_resources,
             *   extractors,
             *   nodes,
             *   resource_forms,
             *   resources
             */

            var sql = ClearDatabaseSqlGenerator.InSchema("resources")
                .ClearTable("inbox_messages")
                .ClearTable("internal_commands")
                .ClearTable("outbox_messages")
                .ClearTable("world_nodes")
                .GenerateSql();

            await connection.ExecuteScalarAsync(sql);
        }
    }
}