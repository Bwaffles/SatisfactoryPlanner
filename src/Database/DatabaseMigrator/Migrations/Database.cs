using Dapper;
using Npgsql;
using System;
using System.Linq;

namespace DatabaseMigrator.Migrations
{
    public static class Database
    {
        public static void EnsureDatabase(string connectionString, string name)
        {
            Console.WriteLine($"Connecting to database '{name}'...");

            var parameters = new DynamicParameters();
            parameters.Add("name", name);

            using var connection = new NpgsqlConnection(connectionString);
            var records = connection.Query("SELECT datname " +
                                             "FROM pg_database " +
                                            "WHERE datname = @name;",
                parameters);

            if (!records.Any())
            { 
                Console.WriteLine($"Database '{name}' does not exist. Creating...");
                connection.Execute($"CREATE DATABASE \"{name}\";");
                Console.WriteLine($"Database '{name}' created.");
            }
        }
    }
}
