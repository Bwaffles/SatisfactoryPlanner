using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.ResourceNodeExtractions
{
    internal class ResourceNodeExtractionEntityTypeConfiguration : IEntityTypeConfiguration<ResourceNodeExtraction>
    {
        public void Configure(EntityTypeBuilder<ResourceNodeExtraction> builder)
        {
            builder.ToTable("resource_node_extractions", "resources");

            builder.HasKey(_ => _.Id);

            builder.Property<ResourceNodeExtractionId>("Id").HasColumnName("id");
            builder.Property<NodeId>("_nodeId").HasColumnName("node_id");
            builder.Property<ExtractorId>("_extractorId").HasColumnName("extractor_id");
            builder.Property<decimal>("_amount").HasColumnName("amount");
            builder.Property<string>("_name").HasColumnName("name");
        }
    }
}
