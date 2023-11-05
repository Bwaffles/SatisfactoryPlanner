using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.WorldNodes
{
    internal class WorldNodeEntityTypeConfiguration : IEntityTypeConfiguration<WorldNode>
    {
        public void Configure(EntityTypeBuilder<WorldNode> builder)
        {
            builder.ToTable("world_nodes", "resources");

            builder.HasKey(_ => _.Id);

            builder.Property<WorldNodeId>("Id").HasColumnName("id");
            builder.Property<WorldId>("_worldId").HasColumnName("world_id");
            builder.Property<NodeId>("_nodeId").HasColumnName("node_id");
            builder.Property<ExtractorId?>("_extractorId").HasColumnName("extractor_id");

            builder.OwnsOne<ExtractionRate>("_extractionRate", b =>
            {
                b.Property(extractionRate => extractionRate.Rate).HasColumnName("extraction_rate");
            });
        }
    }
}
