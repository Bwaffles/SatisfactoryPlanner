using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceExtractors;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodeExtractions;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodes;

namespace SatisfactoryPlanner.Modules.Factories.Infrastructure.Domain.ResourceNodeExtractions
{
    internal class ResourceNodeExtractionEntityTypeConfiguration : IEntityTypeConfiguration<ResourceNodeExtraction>
    {
        public void Configure(EntityTypeBuilder<ResourceNodeExtraction> builder)
        {
            builder.ToTable("resource_node_extractions", "factories");

            builder.HasKey(_ => _.Id);

            builder.Property<ResourceNodeExtractionId>("Id").HasColumnName("id");
            builder.Property<ResourceNodeId>("_resourceNodeId").HasColumnName("resource_node_id");
            builder.Property<ResourceExtractorId>("_resourceExtractorId").HasColumnName("resource_extractor_id");
            builder.Property<decimal>("_amount").HasColumnName("amount");
            builder.Property<string>("_name").HasColumnName("name");
        }
    }
}
