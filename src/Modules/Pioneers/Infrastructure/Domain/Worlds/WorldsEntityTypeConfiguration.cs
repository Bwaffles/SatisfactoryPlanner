using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Worlds;

namespace SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Domain.Worlds
{
    public class WorldsEntityTypeConfiguration : IEntityTypeConfiguration<World>
    {
        public void Configure(EntityTypeBuilder<World> builder)
        {
            builder.ToTable("worlds", "pioneers");

            builder.HasKey(x => x.Id);

            builder.Property<WorldId>("Id").HasColumnName("id");
            builder.Property<string>("_name").HasColumnName("name");
            builder.Property<PioneerId>("_creatorId").HasColumnName("creator_id");
            builder.Property<DateTime>("_createDate").HasColumnName("create_date");
        }
    }
}