using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Linq;

namespace DatabaseMigrator.Migrations
{
    public static class Database
    {
        public static void EnsureDatabase(ILogger logger, string connectionString, string name)
        {
            logger.LogInformation($"Connecting to database '{name}'...");

            var parameters = new DynamicParameters();
            parameters.Add("name", name);

            using var connection = new NpgsqlConnection(connectionString);
            var records = connection.Query("SELECT datname " +
                                             "FROM pg_database " +
                                            "WHERE datname = @name;",
                parameters);

            if (!records.Any())
            {
                logger.LogInformation($"Database '{name}' does not exist. Creating...");
                connection.Execute($"CREATE DATABASE \"{name}\";");
                logger.LogInformation($"Database '{name}' created.");
            }
        }
    }
}
