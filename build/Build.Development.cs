using _build;
using Nuke.Common.Tooling;
using Serilog;
using Utils;
using static Nuke.Common.Tools.Npm.NpmTasks;

partial class Build
{
    const string DevelopmentDatabaseContainerName = "satifactory-planner-development-db";
    bool DevelopmentEnvironmentIsRunning;

    /// <summary>
    ///     Kill any previous docker containers for old runs so we can start fresh.
    /// </summary>
    Target CleanDevelopmentDatabaseContainer => _ => _
        .Unlisted()
        .Executes(() =>
        {
            var containers = DockerPs(s => s.SetFilter($"name={DevelopmentDatabaseContainerName}").SetQuiet(true));
            if (containers.Any())
                DockerKill(config => config.AddContainers(containers.Select(c => c.Text)));

            Log.Information($"Cleaned up previous {DevelopmentDatabaseContainerName} container.");
        });

    /// <summary>
    ///     Pull and run the docker container that hosts the development database.
    ///     After this point we should be able to connect to it using the <see cref="DatabaseServerConnectionString"/>.
    /// </summary>
    Target StartDevelopmentDatabaseContainer => _ => _
        .Unlisted()
        .DependsOn(CleanDevelopmentDatabaseContainer)
        .Executes(() =>
        {
            DockerImagePull(s => s
                .SetName(PostgresImage));

            var databaseConfiguration = DatabaseConfiguration.Development;
            DockerRun(s => s
                .EnableRm()
                .SetName(DevelopmentDatabaseContainerName)
                .SetImage(PostgresImage)
                .AddEnv($"POSTGRES_USER={databaseConfiguration.User}")
                .AddEnv($"POSTGRES_PASSWORD={databaseConfiguration.Password}")
                .SetPublish($"{databaseConfiguration.Port}:5432")
                .SetDetach(true));
                //.SetMount($"type=bind,source=\"{InputFilesDirectory}\",target=/{InputFilesDirectoryName},readonly")));

            PostgresReadinessChecker.WaitForPostgresServer(databaseConfiguration.ServerConnectionString);

            Log.Information($"Started {DevelopmentDatabaseContainerName} container.");
        });

    /// <summary>
    ///     Run the database migrator app to execute all current migrations and bring the new database up to the most recent version.
    /// </summary>
    Target CreateDevelopmentDatabase => _ => _
        .Unlisted()
        .DependsOn(StartDevelopmentDatabaseContainer)
        .Executes(() =>
        {
            var databaseConfiguration = DatabaseConfiguration.Development;
            var databaseMigratorProject = Solution.GetProject("DatabaseMigrator");
            DotNetRun(s => s
                .SetProjectFile(databaseMigratorProject)
                .SetProcessWorkingDirectory(Solution.Directory!.Parent)
                .SetConfiguration(Configuration)
                .SetApplicationArguments($"release \"{databaseConfiguration.ServerConnectionString}\" \"{databaseConfiguration.ConnectionString}\""));
        });

    Target StartApi => _ => _
        .Unlisted()
        .DependsOn(CreateDevelopmentDatabase)
        .DependsOn(CompileSolution)
        .Executes(() =>
        {
            Task.Run(() =>
            {
                var apiProject = Solution.GetProject("SatisfactoryPlanner.API")!;
                DotNet("watch run", apiProject.Directory);

            });
        });

    Target StartApp => _ => _
        .Unlisted()
        .Executes(() =>
        {
            Task.Run(() =>
            {
                var uiProject = Solution.GetProject("SatisfactoryPlanner.UI")!;
                Npm("start", uiProject.Directory);
            });
        });

    Target StartDevelopmentEnvironment => _ => _
        .DependsOn(StartApi)
        .DependsOn(StartApp)
        .Executes(() =>
        {
            DevelopmentEnvironmentIsRunning = true;

            Console.CancelKeyPress += (s, a) =>
            {
                // kill dotnet watch
                // kill react
                a.Cancel = true;
                DevelopmentEnvironmentIsRunning = false;
            };

            while (DevelopmentEnvironmentIsRunning)
                Thread.Sleep(10);
        });
}