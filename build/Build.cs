using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[GitHubActions(
    "continuous",
    GitHubActionsImage.UbuntuLatest,
    On = new[] { GitHubActionsTrigger.Push, GitHubActionsTrigger.PullRequest },
    InvokedTargets = new[] { "BuildAndUnitTests", nameof(RunAllIntegrationTests) },
    PublishArtifacts = true)]
partial class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    
    Target Restore => _ => _
        .Executes(() =>
        {
            // When I restore the solution, I get a warning about the Satisfactory.UI project being invalid
            var projects = Solution.GetProjects("*");
            foreach (var project in projects)
            {
                DotNetRestore(s => s
                    .SetProjectFile(project));
            }
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target UnitTests => _ => _
        .DependsOn(Compile)
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

    Target ArchitectureTests => _ => _
        .DependsOn(UnitTests)
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

    Target BuildAndUnitTests => _ => _
        .Triggers(ArchitectureTests)
        .Executes(() =>
        {
        });

}
