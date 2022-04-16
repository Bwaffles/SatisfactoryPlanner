using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.Extractors
{
    internal class ExtractorEntityTypeConfiguration : IEntityTypeConfiguration<Extractor>
    {
        public void Configure(EntityTypeBuilder<Extractor> builder)
        {
            builder.ToTable("extractors", "resources");

            builder.HasKey(_ => _.Id);

            builder.Property<ExtractorId>("Id").HasColumnName("id");
            builder.Property<decimal>("_extractCycleTime").HasColumnName("extract_cycle_time");
            builder.Property<decimal>("_itemsPerCycle").HasColumnName("items_per_cycle");
            builder.Property<decimal>("_maxClockspeed").HasColumnName("max_clockspeed");
            builder.Property<decimal>("_maxClockspeedPerShard").HasColumnName("max_clockspeed_per_shard");
            builder.Property<int>("_maxShards").HasColumnName("max_shards");

            builder.OwnsMany<AllowedResource>("_allowedResources", _ =>
            {
                _.WithOwner().HasForeignKey("ExtractorId");
                _.ToTable("extractor_allowed_resources", "resources");
                _.Property<ResourceId>("ResourceId").HasColumnName("resource_id");
                _.Property<ExtractorId>("ExtractorId").HasColumnName("extractor_id");
                _.HasKey("ExtractorId", "ResourceId");
            });
        }
    }
}
