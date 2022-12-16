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

            builder.OwnsMany<UserRole>("_roles", b =>
            {
                b.WithOwner().HasForeignKey("UserId");
                b.ToTable("user_roles", "users");
                b.Property<UserId>("UserId").HasColumnName("user_id");
                b.Property<string>("Value").HasColumnName("role_code");
                b.HasKey("UserId", "Value");
            });
        }
    }
}