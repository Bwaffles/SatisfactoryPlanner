using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SatisfactoryPlanner.BuildingBlocks.Application.Outbox;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.InternalCommands;
using SatisfactoryPlanner.UserAccess.Domain.UserRegistrations;
using SatisfactoryPlanner.UserAccess.Domain.Users;
using SatisfactoryPlanner.UserAccess.Infrastructure.Domain.UserRegistrations;
using SatisfactoryPlanner.UserAccess.Infrastructure.Outbox;

namespace SatisfactoryPlanner.UserAccess.Infrastructure
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
