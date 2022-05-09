using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.UserAccess.Domain.UserRegistrations;
using System;

namespace SatisfactoryPlanner.UserAccess.Infrastructure.Domain.UserRegistrations
{
    internal class UserRegistrationEntityTypeConfiguration : IEntityTypeConfiguration<UserRegistration>
    {
        public void Configure(EntityTypeBuilder<UserRegistration> builder)
        {
            builder.ToTable("user_registrations", "users");

            builder.HasKey(x => x.Id);

            builder.Property<UserRegistrationId>("Id").HasColumnName("id");
            builder.Property<string>("_login").HasColumnName("login");
            builder.Property<string>("_email").HasColumnName("email");
            builder.Property<string>("_password").HasColumnName("password");
            builder.Property<DateTime>("_registerDate").HasColumnName("register_date");
            builder.Property<DateTime?>("_confirmedDate").HasColumnName("confirmed_date");

            builder.OwnsOne<UserRegistrationStatus>("_status", b =>
                {
                    b.Property(x => x.Value).HasColumnName("status_code");
                });
        }
    }
}
