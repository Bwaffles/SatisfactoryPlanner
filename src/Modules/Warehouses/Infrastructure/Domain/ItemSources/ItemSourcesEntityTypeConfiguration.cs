using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Domain.ItemSources;

public class ItemSourcesEntityTypeConfiguration : IEntityTypeConfiguration<ItemSource>
{
    public void Configure(EntityTypeBuilder<ItemSource> builder)
    {
        builder.ToTable("item_sources", "warehouses");

        builder.HasKey(x => x.Id);

        builder.Property(itemSource => itemSource.Id).HasColumnName("id");
        builder.Property<WorldId>("_worldId").HasColumnName("world_id");

        builder.OwnsOne<Source>("_source", sourceBuilder =>
        {
            sourceBuilder.Property(source => source.Id).HasColumnName("source_id");
            sourceBuilder.Property(source => source.Name).HasColumnName("source_name");
            sourceBuilder.Property(source => source.Type).HasColumnName("source_type").HasConversion<string>();
        });

        builder.OwnsMany<ProducedItem>("_producedItems", producedItemBuilder =>
        {
            producedItemBuilder.WithOwner().HasForeignKey(producedItem => producedItem.ItemSourceId);
            producedItemBuilder.ToTable("produced_items", "warehouses");

            producedItemBuilder.HasKey(a => new { a.ItemSourceId, a.ItemId });

            producedItemBuilder.Property(producedItem => producedItem.ItemSourceId).HasColumnName("item_source_id");
            producedItemBuilder.Property(producedItem => producedItem.ItemId).HasColumnName("item_id");

            producedItemBuilder.OwnsOne<Rate>("_rate", rateBuilder =>
            {
                rateBuilder.Property(rate => rate.Value).HasColumnName("rate");
            });
        });
    }
}