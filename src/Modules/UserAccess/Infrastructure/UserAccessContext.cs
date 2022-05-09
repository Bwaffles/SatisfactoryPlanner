using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SatisfactoryPlanner.BuildingBlocks.Application.Outbox;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.InternalCommands;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Domain.UserRegistrations;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Outbox;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure
{
    public class UserAccessContext : DbContext
    {
        public DbSet<UserRegistration> UserRegistrations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        private readonly ILoggerFactory _loggerFactory;

        public UserAccessContext(DbContextOptions options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserRegistrationEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new InternalCommandEntityTypeConfiguration());
        }
    }
}
