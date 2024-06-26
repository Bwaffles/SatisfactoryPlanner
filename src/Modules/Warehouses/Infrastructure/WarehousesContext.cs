using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SatisfactoryPlanner.BuildingBlocks.Application.Outbox;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.InternalCommands;
using SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure;

public class WarehousesContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;

    public DbSet<InternalCommand> InternalCommands { get; set; }

    public DbSet<ItemSource> ItemSources { get; set; }

    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public WarehousesContext(DbContextOptions options, ILoggerFactory loggerFactory)
        : base(options) =>
        _loggerFactory = loggerFactory;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}