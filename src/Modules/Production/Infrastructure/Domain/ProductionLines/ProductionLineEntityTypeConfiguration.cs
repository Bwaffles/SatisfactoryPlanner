using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Domain.ProductionLines
{
    internal class ProductionLineEntityTypeConfiguration : IEntityTypeConfiguration<ProductionLine>
    {
        public void Configure(EntityTypeBuilder<ProductionLine> builder)
        {
            builder.ToTable("production_lines", "production");

            builder.HasKey(x => x.Id);

            builder.Property<ProductionLineId>("Id").HasColumnName("id");
            builder.Property<WorldId>("_worldId").HasColumnName("world_id");

            builder.OwnsOne<ProductionLineName>("_name", b =>
            {
                b.Property(productionLineName => productionLineName.Value)
                .HasConversion(new CaseInsensitiveStringConverter())
                .HasColumnName("name");
            });
        }
    }
}
