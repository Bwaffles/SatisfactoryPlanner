using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SatisfactoryPlanner.BuildingBlocks.Application.Outbox;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.InternalCommands;
using SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers;
using SatisfactoryPlanner.Modules.Worlds.Domain.Worlds;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure
{
    public class WorldsContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public DbSet<Pioneer> Pioneers { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<World> Worlds { get; set; }

        public WorldsContext(DbContextOptions options, ILoggerFactory loggerFactory)
            : base(options) =>
            _loggerFactory = loggerFactory;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             //optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}