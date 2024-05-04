using Dapper;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using System.Data;

namespace SatisfactoryPlanner.Modules.UserAccess.IntegrationTests.SeedWork
{
    public static class DatabaseClearer
    {
        public static async Task Clear(IDbConnection connection)
        {
            var sql = ClearDatabaseSqlGenerator.InSchema("users")
                .ClearTable("inbox_messages")
                .ClearTable("internal_commands")
                .ClearTable("outbox_messages")
                .ClearTable("users")
                .ClearTable("user_roles")
                .GenerateSql();

            await connection.ExecuteScalarAsync(sql);
        }
    }
}