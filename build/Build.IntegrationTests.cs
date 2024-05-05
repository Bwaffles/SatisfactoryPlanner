using _build;
using Nuke.Common.Tooling;
using Utils;

partial class Build
{
    const string IntegrationTestDatabaseContainerName = "satifactory-planner-integration-test-db";

    /// <summary>
    ///     Kill any previous docker containers for old runs so we can start fresh.
    /// </summary>
    Target CleanIntegrationTestDatabaseContainer => _ => _
        .Unlisted()
        .Executes(() =>
        {
            var containers = DockerPs(s => s.SetFilter($"name={IntegrationTestDatabaseContainerName}").SetQuiet(true));
            if (containers.Any())
                DockerKill(config => config.AddContainers(containers.Select(containers => containers.Text)));
        });

    /// <summary>
    ///     Pull and run the docker container that hosts the postgres database.
    ///     After this point we should be able to connect to it using the <see cref="DatabaseServerConnectionString"/>.
    /// </summary>
    Target StartIntegrationTestDatabaseContainer => _ => _
        .Unlisted()
        .DependsOn(CleanIntegrationTestDatabaseContainer)
        .Executes(() =>
        {
            DockerImagePull(s => s
                .SetName(PostgresImage));

            var databaseConfiguration = DatabaseConfiguration.IntegrationTests;
            DockerRun(s => s
                .EnableRm()
                .SetName(IntegrationTestDatabaseContainerName)
                .SetImage(PostgresImage)
                .SetEnv(
                    $"POSTGRES_USER={databaseConfiguration.User}",
                    $"POSTGRES_PASSWORD={databaseConfiguration.Password}")
                .SetPublish($"{databaseConfiguration.Port}:5432")
                .EnableDetach());

            PostgresReadinessChecker.WaitForPostgresServer(databaseConfiguration.ServerConnectionString);
        });

    /// <summary>
    ///     Run the database migrator app to execute all current migrations and bring the new database up to the most recent version.
    /// </summary>
    Target CreateIntegrationTestDatabase => _ => _
        .Unlisted()
        .DependsOn(StartIntegrationTestDatabaseContainer)
        .Executes(() =>
        {
            var databaseConfiguration = DatabaseConfiguration.IntegrationTests;
            var databaseMigratorProject = Solution.GetProject("DatabaseMigrator");
            DotNetRun(s => s
                .SetProjectFile(databaseMigratorProject)
                .SetConfiguration(Configuration)
                .SetApplicationArguments($"release \"{databaseConfiguration.ServerConnectionString}\" \"{databaseConfiguration.ConnectionString}\"")
                .SetProcessWorkingDirectory(databaseMigratorProject.Directory));
        });

    // ------------------------------------
    // -  Module Integration Tests Setup  -
    // ------------------------------------

    const string Modules = "SatisfactoryPlanner.Modules";

    Target BuildModuleIntegrationTests => _ => _
        .Unlisted()
        .DependsOn(CreateIntegrationTestDatabase)
        .Executes(() =>
        {
            var projects = Solution.GetProjects($"{Modules}.*.IntegrationTests");
            foreach (var project in projects)
            {
                DotNetBuild(s => s
                    .SetProjectFile(project)
                    .DisableNoRestore());
            }
        });

    const string SatisfactoryPlannerDatabaseEnvName = "ASPNETCORE_SatisfactoryPlanner_IntegrationTests_ConnectionString";

    Target RunResourcesIntegrationTests => _ => _
        .Unlisted()
        .ProceedAfterFailure()
        .DependsOn(BuildModuleIntegrationTests)
        .DependsOn(CreateIntegrationTestDatabase)
        .Executes(() =>
        {
            var databaseConfiguration = DatabaseConfiguration.IntegrationTests;

            Environment.SetEnvironmentVariable(
                SatisfactoryPlannerDatabaseEnvName,
                databaseConfiguration.ConnectionString);

            DotNetTest(s => s
                .SetProjectFile(Solution.GetProject($"{Modules}.Resources.IntegrationTests"))
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild());
        });

    Target RunUserAccessIntegrationTests => _ => _
        .Unlisted()
        .ProceedAfterFailure()
        .DependsOn(CompileSolution)
        .DependsOn(CreateIntegrationTestDatabase)
        .Executes(() =>
        {
            var databaseConfiguration = DatabaseConfiguration.IntegrationTests;

            Environment.SetEnvironmentVariable(
                SatisfactoryPlannerDatabaseEnvName,
                databaseConfiguration.ConnectionString);

            DotNetTest(s => s
                .SetProjectFile(Solution.GetProject($"{Modules}.UserAccess.IntegrationTests"))
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild());
        });

    Target RunWorldsIntegrationTests => _ => _
        .Unlisted()
        .ProceedAfterFailure()
        .DependsOn(CompileSolution)
        .DependsOn(CreateIntegrationTestDatabase)
        .Executes(() =>
        {
            var databaseConfiguration = DatabaseConfiguration.IntegrationTests;

            Environment.SetEnvironmentVariable(
                SatisfactoryPlannerDatabaseEnvName,
                databaseConfiguration.ConnectionString);

            DotNetTest(s => s
                .SetProjectFile(Solution.GetProject($"{Modules}.Worlds.IntegrationTests"))
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild());
        });

    Target RunProductionIntegrationTests => _ => _
        .Unlisted()
        .ProceedAfterFailure()
        .DependsOn(CompileSolution)
        .DependsOn(CreateIntegrationTestDatabase)
        .Executes(() =>
        {
            var databaseConfiguration = DatabaseConfiguration.IntegrationTests;

            Environment.SetEnvironmentVariable(
                SatisfactoryPlannerDatabaseEnvName,
                databaseConfiguration.ConnectionString);

            DotNetTest(s => s
                .SetProjectFile(Solution.GetProject($"{Modules}.Production.IntegrationTests"))
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild());
        });

    // ------------------------------------
    // -  Api Integration Tests Setup  -
    // ------------------------------------

    Target RunApiIntegrationTests => _ => _
        .Unlisted()
        .ProceedAfterFailure()
        .DependsOn(CompileSolution)
        .DependsOn(CreateIntegrationTestDatabase)
        .Executes(() =>
        {
            var databaseConfiguration = DatabaseConfiguration.IntegrationTests;

            Environment.SetEnvironmentVariable(
                SatisfactoryPlannerDatabaseEnvName,
                databaseConfiguration.ConnectionString);

            DotNetTest(s => s
                .SetProjectFile(Solution.GetProject($"SatisfactoryPlanner.API.IntegrationTests"))
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild());
        });

    Target RunAllIntegrationTests => _ => _
        .DependsOn(RunResourcesIntegrationTests)
        .DependsOn(RunUserAccessIntegrationTests)
        .DependsOn(RunWorldsIntegrationTests)
        .DependsOn(RunProductionIntegrationTests)
        .DependsOn(RunApiIntegrationTests)
        .Executes(() =>
        {

        });
}