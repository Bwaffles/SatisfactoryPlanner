using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.InternalCommands;
using SatisfactoryPlanner.Modules.Production.Domain.Factories;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure
{
    public class ProductionContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public DbSet<Factory> Factories { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        public ProductionContext(DbContextOptions options, ILoggerFactory loggerFactory)
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