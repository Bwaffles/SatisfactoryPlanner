using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Domain.Users
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", "users");

            builder.HasKey(x => x.Id);

            builder.Property<UserId>("Id").HasColumnName("id");
            builder.Property<string>("_auth0UserId").HasColumnName("auth0_user_id");
        }
    }
}