using DatabaseMigrator.Migrations;
using DatabaseMigrator.Migrations.Factories;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DatabaseMigrator
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            Console.WriteLine("Starting migration...");

            var environment = args.Length > 0 ? args[0] : "debug";
            if (environment != "release" && environment != "debug")
            {
                // release: used from the BuildIntegrationTests Nuke script to run all migrations
                // debug: used during dev to have an interactive experience so we can test rollbacks and migration retests
                WriterCommandArgumentError();
                return -1;
            }

            string serverConnectionString; // to be able to create the DB
            string connectionString; // to be able to create the DB
            if (args.Length > 1)
            {
                serverConnectionString = args[1];
                connectionString = args[2];
            }
            else
            {
                var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

                serverConnectionString = config.GetConnectionString("Server");
                connectionString = config.GetConnectionString("SatisfactoryPlanner");
            }

            Database.EnsureDatabase(serverConnectionString, "satisfactory-planner");

            var servicesProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(Create_Factories).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                //.AddScoped<PostgresQuoter, NoQuoteQuoter>()
                // Build the service provider
                .BuildServiceProvider(false);

            using (var scope = servicesProvider.CreateScope())
            {
                UpdateDatabase(connectionString, scope.ServiceProvider, environment);
            }

            return 0;
        }

        private static void WriterCommandArgumentError() =>
            Console.WriteLine(
                "Invalid arguments. Execution: DatabaseMigrator [(release|debug)] [serverConnectionString] [connectionString].\r\n");

        private static void UpdateDatabase(string connectionString, IServiceProvider serviceProvider, string environment)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            if (environment == "release")
            {
                runner.MigrateUp();
                return;
            }
            
            runner.ListMigrations();

            bool quit = false;
            while (!quit)
            {
                quit = SelectOption(runner);
            }
        }

        private static bool SelectOption(IMigrationRunner runner)
        {
            Console.WriteLine("\r\nSelect option\r\n" +
                              "\t(1) Rollback\r\n" +
                              "\t(2) Migrate Next\r\n" +
                              "\t(3) Retest Last Migration\r\n" +
                              "\t(Q) Quit:");

            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    RollbackMigrations(runner);
                    runner.ListMigrations();
                    return false;
                case "2":
                    MigrateNext(runner);
                    runner.ListMigrations();
                    return false;
                case "3":
                    RetestPreviousMigrations(runner);
                    runner.ListMigrations();
                    return false;
                case "Q":
                case "q":
                    return true;
                default:
                    Console.WriteLine("Invalid option.");
                    return false;

            }
        }

        private static void RollbackMigrations(IMigrationRunner runner, int steps = 1)
        {
            runner.Rollback(steps);
        }

        private static void MigrateNext(IMigrationRunner runner, long? version = null)
        {
            if (version == null)
                runner.MigrateUp();
            else
                runner.MigrateUp(version.Value);
        }

        private static void RetestPreviousMigrations(IMigrationRunner runner)
        {
            runner.Rollback(1);
            runner.ListMigrations();
            runner.MigrateUp();
        }
    }
}
