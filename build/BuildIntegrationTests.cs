using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Tools.Docker;
using Nuke.Common.Tools.DotNet;
using Utils;

partial class Build
{
    const string ContainerName = "postgres-test-db";

    /// <summary>
    ///     Kill any previous docker containers for old runs so we can start fresh.
    /// </summary>
    Target CleanDatabaseContainer => _ => _
        .Executes(() =>
        {
            var containers = DockerTasks.DockerPs(s => s.SetFilter($"name={ContainerName}").SetQuiet(true));
            if (containers.Any())
            {
                DockerTasks.DockerKill(config => config
                    .AddContainers(containers.Select(containers => containers.Text))
                );
            }
        });

    const string PostgresImage = "postgres:13.3";
    const string PostgresPassword = "123qwe!@#QWE";
    const string PostgresUser = "test-user";
    const string PostgresPort = "1401";
    const string MasterConnectionString = $"Server=127.0.0.1:{PostgresPort};User Id={PostgresUser};Password={PostgresPassword};";

    /// <summary>
    ///     Pull and run the docker container that hosts the postgres database. After this point we should be able to
    /// connect to it using the <see cref="MasterConnectionString"/>.
    /// </summary>
    Target PreparePostgresContainer => _ => _
        .DependsOn(CleanDatabaseContainer)
        .Executes(() =>
        {
            DockerTasks.DockerImagePull(s => s
                .SetName(PostgresImage));

            DockerTasks.DockerRun(s => s
                .EnableRm()
                .SetName(ContainerName)
                .SetImage(PostgresImage)
                .SetEnv(
                    $"POSTGRES_USER={PostgresUser}",
                    $"POSTGRES_PASSWORD={PostgresPassword}")
                .SetPublish($"{PostgresPort}:5432")
                //.SetMount($"type=bind,source=\"{InputFilesDirectory}\",target=/{InputFilesDirectoryName},readonly")
                .EnableDetach());

            PostgresReadinessChecker.WaitForPostgresServer(MasterConnectionString);
        });

    const string ConnectionString = $"{MasterConnectionString};Database=satisfactory-planner;";
    /// <summary>
    ///     Run the database migrator app to execute all current migrations and bring the new database up to the most recent version.
    /// </summary>
    Target CreateDatabase => _ => _
        .DependsOn(PreparePostgresContainer)
        .Executes(() =>
        {
            var databaseMigratorProject = Solution.GetProject("DatabaseMigrator");
            DotNetTasks.DotNetRun(s => s
                .SetProjectFile(databaseMigratorProject)
                .SetConfiguration(Configuration)
                .SetApplicationArguments($"release \"{MasterConnectionString}\" \"{ConnectionString}\""));
        });

    const string Modules = "SatisfactoryPlanner.Modules";
    const string WorldsModuleIntegrationTestsProjectName = $"{Modules}.Worlds.IntegrationTests";

    Target BuildWorldsModuleIntegrationTests => _ => _
        .DependsOn(CreateDatabase)
        .Executes(() =>
        {
            var integrationTest = Solution.GetProject(WorldsModuleIntegrationTestsProjectName);

            DotNetTasks.DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    const string SatisfactoryPlannerDatabaseEnvName = "ASPNETCORE_SatisfactoryPlanner_IntegrationTests_ConnectionString";

    Target RunWorldsModuleIntegrationTests => _ => _
        .DependsOn(BuildWorldsModuleIntegrationTests)
        .Executes(() =>
        {
            var integrationTest = Solution.GetProject(WorldsModuleIntegrationTestsProjectName);
            Environment.SetEnvironmentVariable(
                SatisfactoryPlannerDatabaseEnvName,
                ConnectionString);

            DotNetTasks.DotNetTest(s => s
                .EnableNoBuild()
                .SetProjectFile(integrationTest));
        });
    
    const string UserAccessModuleIntegrationTestsProjectName = $"{Modules}.UserAccess.IntegrationTests";

    Target BuildUserAccessModuleIntegrationTests => _ => _
        .DependsOn(CreateDatabase)
        .Executes(() =>
        {
            var integrationTest = Solution.GetProject(UserAccessModuleIntegrationTestsProjectName);

            DotNetTasks.DotNetBuild(s => s
                .SetProjectFile(integrationTest)
                .DisableNoRestore());
        });

    Target RunUserAccessModuleIntegrationTests => _ => _
        .DependsOn(BuildUserAccessModuleIntegrationTests)
        .Executes(() =>
        {
            var integrationTest = Solution.GetProject(UserAccessModuleIntegrationTestsProjectName);
            Environment.SetEnvironmentVariable(
                SatisfactoryPlannerDatabaseEnvName,
                ConnectionString);

            DotNetTasks.DotNetTest(s => s
                .EnableNoBuild()
                .SetProjectFile(integrationTest));
        });

    // ReSharper disable once UnusedMember.Local because it's called from the buildPipeline script for my CI Pipeline git Action
    Target RunAllIntegrationTests => _ => _
        .DependsOn(RunWorldsModuleIntegrationTests)
        .DependsOn(RunUserAccessModuleIntegrationTests)
        .Executes(() =>
        {

        });
}