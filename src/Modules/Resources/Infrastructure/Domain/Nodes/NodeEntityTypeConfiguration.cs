using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Domain.Nodes
{
    internal class NodeEntityTypeConfiguration : IEntityTypeConfiguration<Node>
    {
        public void Configure(EntityTypeBuilder<Node> builder)
        {
            builder.ToTable("nodes", "resources");

            builder.HasKey(_ => _.Id);

            builder.Property<NodeId>("Id").HasColumnName("id");
            builder.Property<ResourceId>("_resourceId").HasColumnName("resource_id");

            builder.OwnsOne<NodePurity>("_purity", b =>
            {
                b.Property(nodePurity => nodePurity.Value).HasColumnName("purity");
            });
        }
    }
}