using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceExtractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.ResourceExtractors
{
    internal class ResourceExtractorEntityTypeConfiguration : IEntityTypeConfiguration<ResourceExtractor>
    {
        public void Configure(EntityTypeBuilder<ResourceExtractor> builder)
        {
            builder.ToTable("resource_extractors", "resources");

            builder.HasKey(_ => _.Id);

            builder.Property<ResourceExtractorId>("Id").HasColumnName("id");
            builder.Property<decimal>("_extractCycleTime").HasColumnName("extract_cycle_time");
            builder.Property<decimal>("_itemsPerCycle").HasColumnName("items_per_cycle");
            builder.Property<decimal>("_maxClockspeed").HasColumnName("max_clockspeed");
            builder.Property<decimal>("_maxClockspeedPerShard").HasColumnName("max_clockspeed_per_shard");
            builder.Property<int>("_maxShards").HasColumnName("max_shards");

            builder.OwnsMany<ResourceExtractorAllowedResource>("_allowedResources", _ =>
            {
                _.WithOwner().HasForeignKey("_resourceExtractorId");
                _.ToTable("resource_extractor_allowed_resources", "resources");
                _.Property<ResourceExtractorAllowedResourceId>("Id").HasColumnName("id");
                _.HasKey("Id");
                _.Property<ResourceId>("_resourceId").HasColumnName("item_id");
                _.Property<ResourceExtractorId>("_resourceExtractorId").HasColumnName("resource_extractor_id");
            });
        }
    }
}
