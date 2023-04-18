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

            builder.OwnsOne<ExtractorCycle>("_cycle", b =>
            {
                b.Property(extractorCycle => extractorCycle.ResourcesExtracted)
                    .HasColumnName("resources_extracted_per_cycle");
                b.Property(extractorCycle => extractorCycle.SecondsToComplete)
                    .HasColumnName("seconds_to_complete_cycle");
            });

            builder.OwnsOne<ExtractorClockspeed>("_clockspeed", b =>
            {
                b.Property(extractorClockspeed => extractorClockspeed.DefaultClockspeed)
                    .HasColumnName("default_clockspeed");
                b.Property(extractorClockspeed => extractorClockspeed.MaxShards).HasColumnName("max_shards");
                b.Property(extractorClockspeed => extractorClockspeed.OverclockPerShard)
                    .HasColumnName("overclock_per_shard");
            });

            builder.OwnsMany<AllowedResource>("_allowedResources", b =>
            {
                b.WithOwner().HasForeignKey("ExtractorId");
                b.ToTable("extractor_allowed_resources", "resources");

                b.HasKey("ExtractorId", "ResourceId");

                b.Property<ExtractorId>("ExtractorId").HasColumnName("extractor_id");
                b.Property<ResourceId>("ResourceId").HasColumnName("resource_id");
            });
        }
    }
}