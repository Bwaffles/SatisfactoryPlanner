using static Nuke.Common.Tools.Npm.NpmTasks;

partial class Build
{
    bool DevelopmentEnvironmentIsRunning;

    Target CleanupDockerProjects => _ => _
        .Unlisted()
        .Executes(() =>
        {
            Docker("compose down", Solution.Directory);
        });

    /// <summary>
    ///     Run the database migrator app to execute all current migrations and bring the new database up to the most recent version.
    /// </summary>
    Target CreateDevelopmentDatabase => _ => _
        .Unlisted()
        .DependsOn(CleanupDockerProjects)
        .Executes(() =>
        {
            Docker("compose up db db-migrator -d --build", Solution.Directory);
        });
    
    Target RunDockerCompose => _ => _
        .Unlisted()
        .DependsOn(CleanupDockerProjects)
        .Executes(() => 
        {
            Docker("compose up -d --build", Solution.Directory);
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
        .After(StartApi, RunDockerCompose) // run app last since all other services are required first
        .Executes(() =>
        {
            Task.Run(() =>
            {
                var uiProject = Solution.GetProject("SatisfactoryPlanner.UI")!;
                Npm("start", uiProject.Directory);
            });
        });

    Target StartDevelopmentEnvironment => _ => _
        .DependsOn(StartApi, StartApp)
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

    Target StartApplication => _ => _
        .DependsOn(RunDockerCompose)
        .DependsOn(StartApp)
        .Executes(() =>
        {
            DevelopmentEnvironmentIsRunning = true;

            Console.CancelKeyPress += (s, a) =>
            {
                // kill react
                a.Cancel = true;
                DevelopmentEnvironmentIsRunning = false;
            };

            while (DevelopmentEnvironmentIsRunning)
                Thread.Sleep(10);
        });

    /// <summary>
    ///     Start only the api in a container. Useful when doing front end work and just need to keep the server running.
    /// </summary>
    Target StartApiContainer => _ => _
        .DependsOn(RunDockerCompose)
        .Executes(() => {});
}