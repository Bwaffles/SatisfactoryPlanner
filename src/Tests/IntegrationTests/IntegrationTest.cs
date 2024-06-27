using FluentAssertions.Execution;
using SatisfactoryPlanner.BuildingBlocks.EventBus;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Infrastructure;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;
using SatisfactoryPlanner.Modules.Warehouses.Infrastructure;
using SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;

namespace SatisfactoryPlanner.IntegrationTests;

[TestFixture]
public class IntegrationTest
{
    protected string ConnectionString { get; private set; } = null!;

    public IEventsBus EventsBus { get; private set; } = default!;

    private Logger Logger { get; set; } = null!;

    protected IResourcesModule ResourcesModule { get; private set; } = null!;

    protected IWarehousesModule WarehousesModule { get; private set; } = null!;

    protected ExecutionContextMock ExecutionContext { get; private set; } = null!;

    [SetUp]
    public async Task BeforeEachTest()
    {
        const string connectionStringEnvironmentVariable = "ASPNETCORE_SatisfactoryPlanner_IntegrationTests_ConnectionString";
        ConnectionString = EnvironmentVariablesProvider.GetVariable(connectionStringEnvironmentVariable);

        await DatabaseClearer.Clear(ConnectionString);

        Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.File(new CompactJsonFormatter(),
                "logs/logs.json",
                rollOnFileSizeLimit: true,
                fileSizeLimitBytes: 10 * 1024 * 1024)
            .CreateLogger();

        ExecutionContext = new ExecutionContextMock(Guid.NewGuid());
        EventsBus = new InMemoryEventBusClient();

        ResourcesStartup.Start(
            ConnectionString,
            ExecutionContext,
            Logger,
            EventsBus,
            new ResourcesConfiguration()
            {
                InternalProcessingExecutionInterval = TimeSpan.FromMilliseconds(200)
            });

        ResourcesModule = new ResourcesModule();

        WarehousesStartup.Start(
            ConnectionString,
            ExecutionContext,
            Logger,
            EventsBus,
            new WarehousesConfiguration()
            {
                InternalProcessingExecutionInterval = TimeSpan.FromMilliseconds(200)
            });

        WarehousesModule = new WarehousesModule();
    }

    [TearDown]
    public void AfterEachTest()
    {
        EventsBus.Stop();
        EventsBus.Dispose();

        Logger.Dispose();

        ResourcesStartup.Stop();
        WarehousesStartup.Stop();
    }

    protected static void AssertAll(Action assert)
    {
        using (new AssertionScope())
            assert();
    }
}