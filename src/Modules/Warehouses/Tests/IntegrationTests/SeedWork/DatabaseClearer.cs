using Dapper;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using System.Data;

namespace SatisfactoryPlanner.Modules.Warehouses.IntegrationTests.SeedWork;

public static class DatabaseClearer
{
    public static async Task Clear(IDbConnection connection)
    {
        var sql = ClearDatabaseSqlGenerator.InSchema("warehouses")
            .ClearTable("inbox_messages")
            .ClearTable("internal_commands")
            //.ClearTable("outbox_messages")
            .ClearTable("item_sources")
            .ClearTable("produced_items")
            .GenerateSql();

        await connection.ExecuteScalarAsync(sql);
    }
}