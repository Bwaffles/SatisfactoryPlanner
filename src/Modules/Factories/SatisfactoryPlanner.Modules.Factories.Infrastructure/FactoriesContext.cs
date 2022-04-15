using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.InternalCommands;
using SatisfactoryPlanner.Modules.Factories.Domain.Factories;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceExtractors;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodeExtractions;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodes;

namespace SatisfactoryPlanner.Modules.Factories.Infrastructure
{
    public class FactoriesContext : DbContext
    {
        public DbSet<Factory> Factories { get; set; }

        public DbSet<ResourceExtractor> ResourceExtractors { get; set; }

        public DbSet<ResourceNodeExtraction> ResourceNodeExtractions { get; set; }

        public DbSet<ResourceNode> ResourceNodes { get; set; }
        
        public DbSet<InternalCommand> InternalCommands { get; set; }

        private readonly ILoggerFactory _loggerFactory;

        public FactoriesContext(DbContextOptions options, ILoggerFactory loggerFactory)
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
