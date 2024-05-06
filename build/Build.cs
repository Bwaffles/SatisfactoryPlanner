using Nuke.Common.ProjectModel;

partial class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.StartDevelopmentEnvironment);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = Configuration.Release; // Always using release to be as close to production as possible

    [Solution] readonly Solution Solution;

    const string PostgresImage = "postgres:13.3";

    Target CompileSolution => _ => _
        .Unlisted()
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration));
        });

    Target RunUnitTests => _ => _
        .Unlisted()
        .ProceedAfterFailure()
        .DependsOn(CompileSolution)
        .Executes(() =>
        {
            var projects = Solution.GetProjects("*.UnitTests");
            foreach (var project in projects)
            {
                DotNetTest(_ => _
                    .SetProjectFile(project)
                    .SetConfiguration(Configuration)
                    .EnableNoRestore()
                    .EnableNoBuild()
                );
            }
        });

    Target RunArchitectureTests => _ => _
        .Unlisted()
        .ProceedAfterFailure()
        .DependsOn(CompileSolution)
        .Executes(() =>
        {
            var projects = Solution.GetProjects("*.ArchTests");
            foreach (var project in projects)
            {
                DotNetTest(_ => _
                    .SetProjectFile(project)
                    .SetConfiguration(Configuration)
                    .EnableNoRestore()
                    .EnableNoBuild()
                );
            }
        });

    // ReSharper disable once UnusedMember.Local because it's called from the buildPipeline script for my CI Pipeline git Action
    Target RunCIBuild => _ => _
        .DependsOn(RunAllIntegrationTests)
        .DependsOn(RunUnitTests)
        .DependsOn(RunArchitectureTests)
        .Executes(() =>
        {
        });

}
