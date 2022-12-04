using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers;
using SatisfactoryPlanner.Modules.Worlds.Domain.Worlds;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure.Domain.Worlds
{
    public class WorldsEntityTypeConfiguration : IEntityTypeConfiguration<World>
    {
        public void Configure(EntityTypeBuilder<World> builder)
        {
            builder.ToTable("worlds", "worlds");

            builder.HasKey(x => x.Id);

            builder.Property<WorldId>("Id").HasColumnName("id");
            builder.Property<string>("_name").HasColumnName("name");
            builder.Property<PioneerId>("_creatorId").HasColumnName("creator_id");
            builder.Property<DateTime>("_createDate").HasColumnName("create_date");
        }
    }
}