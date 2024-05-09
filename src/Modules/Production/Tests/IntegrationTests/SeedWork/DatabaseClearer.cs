using Dapper;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using System.Data;

namespace SatisfactoryPlanner.Modules.Production.IntegrationTests.SeedWork
{
    public static class DatabaseClearer
    {
        public static async Task Clear(IDbConnection connection)
        {
            /* Not clearing the following tables since they're pre-loaded reference tables:
             */
            var sql = ClearDatabaseSqlGenerator.InSchema("production")
                .ClearTable("inbox_messages")
                .ClearTable("internal_commands")
                .ClearTable("outbox_messages")
                .ClearTable("production_lines")
                .GenerateSql();

            await connection.ExecuteScalarAsync(sql);
        }
    }
}
