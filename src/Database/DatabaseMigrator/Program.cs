using Microsoft.Extensions.Configuration;
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

            var runner = new MigrationRunner(serverConnectionString, connectionString);

            UpdateDatabase(runner, environment);

            return 0;
        }

        private static void WriterCommandArgumentError() =>
            Console.WriteLine(
                "Invalid arguments. Execution: DatabaseMigrator [(release|debug)] [serverConnectionString] [connectionString].\r\n");

        private static void UpdateDatabase(MigrationRunner runner, string environment)
        {
            if (environment == "release")
            {
                runner.Migrate();
                return;
            }
            
            runner.ListMigrations();

            bool quit = false;
            while (!quit)
            {
                quit = SelectOption(runner);
            }
        }

        private static bool SelectOption(MigrationRunner runner)
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
                    runner.Rollback();
                    runner.ListMigrations();
                    return false;
                case "2":
                    runner.Migrate();
                    runner.ListMigrations();
                    return false;
                case "3":
                    runner.RerunPreviousMigration();
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
    }
}
