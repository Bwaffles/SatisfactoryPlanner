using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SatisfactoryPlanner.BuildingBlocks.Application.Outbox;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.InternalCommands;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure
{
    public class UserAccessContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public DbSet<User> Users { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        public UserAccessContext(DbContextOptions options, ILoggerFactory loggerFactory)
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