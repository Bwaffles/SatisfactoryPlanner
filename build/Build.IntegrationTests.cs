using _build;
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
                .SetApplicationArguments($"release \"{databaseConfiguration.ServerConnectionString}\" \"{databaseConfiguration.ConnectionString}\""));
        });

    // ------------------------------------
    // -  Module Integration Tests Setup  -
    // ------------------------------------

    const string Modules = "SatisfactoryPlanner.Modules";
    const string SatisfactoryPlannerDatabaseEnvName = "ASPNETCORE_SatisfactoryPlanner_IntegrationTests_ConnectionString";

    const string ResourcesModuleIntegrationTestsProjectName = $"{Modules}.Resources.IntegrationTests";

    Target BuildResourcesModuleIntegrationTests => _ => _
        .Unlisted()
        .DependsOn(CreateIntegrationTestDatabase)
        .Executes(() =>
        {
            var integrationTest = Solution.GetProject(ResourcesModuleIntegrationTestsProjectName);

            DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    Target RunResourcesModuleIntegrationTests => _ => _
        .Unlisted()
        .DependsOn(BuildResourcesModuleIntegrationTests)
        .Executes(() =>
        {
            var databaseConfiguration = DatabaseConfiguration.IntegrationTests;
            var integrationTest = Solution.GetProject(ResourcesModuleIntegrationTestsProjectName);
            Environment.SetEnvironmentVariable(
                SatisfactoryPlannerDatabaseEnvName,
                databaseConfiguration.ConnectionString);

            DotNetTest(s => s
                .EnableNoBuild()
                .SetProjectFile(integrationTest));
        });

    const string UserAccessModuleIntegrationTestsProjectName = $"{Modules}.UserAccess.IntegrationTests";

    Target BuildUserAccessModuleIntegrationTests => _ => _
        .Unlisted()
        .DependsOn(CreateIntegrationTestDatabase)
        .Executes(() =>
        {
            var integrationTest = Solution.GetProject(UserAccessModuleIntegrationTestsProjectName);

            DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    Target RunUserAccessModuleIntegrationTests => _ => _
        .Unlisted()
        .DependsOn(BuildUserAccessModuleIntegrationTests)
        .Executes(() =>
        {
            var databaseConfiguration = DatabaseConfiguration.IntegrationTests;
            var integrationTest = Solution.GetProject(UserAccessModuleIntegrationTestsProjectName);
            Environment.SetEnvironmentVariable(
                SatisfactoryPlannerDatabaseEnvName,
                databaseConfiguration.ConnectionString);

            DotNetTest(s => s
                .EnableNoBuild()
                .SetProjectFile(integrationTest));
        });

    const string WorldsModuleIntegrationTestsProjectName = $"{Modules}.Worlds.IntegrationTests";

    Target BuildWorldsModuleIntegrationTests => _ => _
        .Unlisted()
        .DependsOn(CreateIntegrationTestDatabase)
        .Executes(() =>
        {
            var integrationTest = Solution.GetProject(WorldsModuleIntegrationTestsProjectName);

            DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    Target RunWorldsModuleIntegrationTests => _ => _
        .Unlisted()
        .DependsOn(BuildWorldsModuleIntegrationTests)
        .Executes(() =>
        {
            var databaseConfiguration = DatabaseConfiguration.IntegrationTests;
            var integrationTest = Solution.GetProject(WorldsModuleIntegrationTestsProjectName);
            Environment.SetEnvironmentVariable(
                SatisfactoryPlannerDatabaseEnvName,
                databaseConfiguration.ConnectionString);

            DotNetTest(s => s
                .EnableNoBuild()
                .SetProjectFile(integrationTest));
        });

    // ReSharper disable once UnusedMember.Local because it's called from the buildPipeline script for my CI Pipeline git Action
    Target RunAllIntegrationTests => _ => _
        .DependsOn(RunResourcesModuleIntegrationTests)
        .DependsOn(RunUserAccessModuleIntegrationTests)
        .DependsOn(RunWorldsModuleIntegrationTests)
        .Executes(() =>
        {

        });
}