using Dapper;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using System.Data;

namespace SatisfactoryPlanner.Modules.Worlds.IntegrationTests.SeedWork
{
    public static class DatabaseClearer
    {
        public static async Task Clear(IDbConnection connection)
        {
            var sql = ClearDatabaseSqlGenerator.InSchema("worlds")
                .ClearTable("inbox_messages")
                .ClearTable("internal_commands")
                .ClearTable("outbox_messages")
                .ClearTable("pioneers")
                .ClearTable("worlds")
                .GenerateSql();

            await connection.ExecuteScalarAsync(sql);
        }
    }
}
