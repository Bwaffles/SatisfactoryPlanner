using Npgsql;
using Production = SatisfactoryPlanner.Modules.Production.IntegrationTests.SeedWork;
using UserAccess = SatisfactoryPlanner.Modules.UserAccess.IntegrationTests.SeedWork;
using Worlds = SatisfactoryPlanner.Modules.Worlds.IntegrationTests.SeedWork;
using Resources = SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.API.IntegrationTests
{
    public static class DatabaseClearer
    {
        public static async Task Clear(string connectionString)
        {
            await using var connection = new NpgsqlConnection(connectionString);

            // Call out to each module's integration test project so that the api tests don't need to stay in sync with all module changes.
            // Assuming each integration test project knows how to clear its own database data between tests.
            await UserAccess.DatabaseClearer.Clear(connection);
            await Worlds.DatabaseClearer.Clear(connection);
            await Resources.DatabaseClearer.Clear(connection);
            await Production.DatabaseClearer.Clear(connection);
        }
    }
}