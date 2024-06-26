using Npgsql;
using Production = SatisfactoryPlanner.Modules.Production.IntegrationTests.SeedWork;
using Resources = SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork;
using UserAccess = SatisfactoryPlanner.Modules.UserAccess.IntegrationTests.SeedWork;
using Warehouses = SatisfactoryPlanner.Modules.Warehouses.IntegrationTests.SeedWork;
using Worlds = SatisfactoryPlanner.Modules.Worlds.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.IntegrationTests;

public static class DatabaseClearer
{
    public static async Task Clear(string connectionString)
    {
        await using var connection = new NpgsqlConnection(connectionString);

        await Production.DatabaseClearer.Clear(connection);
        await Resources.DatabaseClearer.Clear(connection);
        await UserAccess.DatabaseClearer.Clear(connection);
        await Warehouses.DatabaseClearer.Clear(connection);
        await Worlds.DatabaseClearer.Clear(connection);
    }
}