using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.TappedNodes
{
    internal class TappedNodeEntityTypeConfiguration : IEntityTypeConfiguration<TappedNode>
    {
        public void Configure(EntityTypeBuilder<TappedNode> builder)
        {
            builder.ToTable("tapped_nodes", "resources");

            builder.HasKey(_ => _.Id);

            builder.Property<ResourceNodeExtractionId>("Id").HasColumnName("id");
            builder.Property<WorldId>("_worldId").HasColumnName("world_id");
            builder.Property<NodeId>("_nodeId").HasColumnName("node_id");
            builder.Property<ExtractorId>("_extractorId").HasColumnName("extractor_id");
            builder.Property<decimal>("_amountToExtract").HasColumnName("amount_to_extract");
            builder.Property<string>("_name").HasColumnName("name");
        }
    }
}
