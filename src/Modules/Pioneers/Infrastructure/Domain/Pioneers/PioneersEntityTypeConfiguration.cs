using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers;

namespace SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Domain.Pioneers
{
    internal class PioneersEntityTypeConfiguration : IEntityTypeConfiguration<Pioneer>
    {
        public void Configure(EntityTypeBuilder<Pioneer> builder)
        {
            builder.ToTable("pioneers", "pioneers");

            builder.HasKey(x => x.Id);

            builder.Property<PioneerId>("Id").HasColumnName("id");
            builder.Property<string>("Auth0UserId").HasColumnName("auth0_user_id");
        }
    }
}