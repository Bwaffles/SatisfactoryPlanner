using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SatisfactoryPlanner.BuildingBlocks.Application.Outbox;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.InternalCommands;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure
{
    public class ResourcesContext : DbContext
    {
        public DbSet<Node> Nodes { get; set; }

        public DbSet<TappedNode> TappedNodes { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        private readonly ILoggerFactory _loggerFactory;

        public ResourcesContext(DbContextOptions options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
