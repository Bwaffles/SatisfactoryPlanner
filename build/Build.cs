using Nuke.Common.ProjectModel;

partial class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.StartDevelopmentEnvironment);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;

    const string PostgresImage = "postgres:13.3";

    Target RestoreSolution => _ => _
        .Unlisted()
        .Executes(() =>
        {
            // When I restore the solution, I get a warning about the Satisfactory.UI project being invalid
            //var projects = Solution.GetProjects("*").Where(project => project.Name != "_build");
            //foreach (var project in projects)
            //{
            //    DotNetRestore(s => s
            //        .SetProjectFile(project));
            //}

            DotNetRestore(s => s.SetProjectFile(Solution));
        });

    Target CompileSolution => _ => _
        .Unlisted()
        .DependsOn(RestoreSolution)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target RunUnitTests => _ => _
        .Unlisted()
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
        .DependsOn(CompileSolution)
        .DependsOn(RunArchitectureTests)
        .DependsOn(RunUnitTests)
        .DependsOn(RunAllIntegrationTests)
        .Executes(() =>
        {
        });

}
