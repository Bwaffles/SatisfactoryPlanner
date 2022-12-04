using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure.Domain.Pioneers
{
    internal class PioneersEntityTypeConfiguration : IEntityTypeConfiguration<Pioneer>
    {
        public void Configure(EntityTypeBuilder<Pioneer> builder)
        {
            builder.ToTable("pioneers", "worlds");

            builder.HasKey(x => x.Id);

            builder.Property<PioneerId>("Id").HasColumnName("id");
        }
    }
}