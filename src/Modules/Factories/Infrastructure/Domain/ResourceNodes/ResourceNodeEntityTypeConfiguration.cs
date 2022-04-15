using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodes;
using SatisfactoryPlanner.Modules.Factories.Domain.Resources;

namespace SatisfactoryPlanner.Modules.Factories.Infrastructure.Domain.ResourceNodes
{
    internal class ResourceNodeEntityTypeConfiguration : IEntityTypeConfiguration<ResourceNode>
    {
        public void Configure(EntityTypeBuilder<ResourceNode> builder)
        {
            builder.ToTable("resource_nodes", "factories");

            builder.HasKey(_ => _.Id);

            builder.Property<ResourceNodeId>("Id").HasColumnName("id");

            builder.OwnsOne<ResourceNodePurity>("_purity", _ =>
            {
                _.Property(p => p.Value).HasColumnName("purity");
            });

            builder.Property<ResourceId>("_resourceId").HasColumnName("item_id");
        }
    }
}
