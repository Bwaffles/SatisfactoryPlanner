using FluentMigrator;

namespace DatabaseMigrator.Migrations.Warehouses.ItemSources;

[Migration(202406251149)]
public class Create_ProducedItems : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("produced_items")
            .InSchema("warehouses")
            .WithColumn("item_source_id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("item_id").AsString().NotNullable().PrimaryKey()
            .WithColumn("rate").AsDecimal().NotNullable()
            ;

        Create.ForeignKey("FK_produced_items_item_source_id")
            .FromTable("produced_items").InSchema("warehouses").ForeignColumn("item_source_id")
            .ToTable("item_sources").InSchema("warehouses").PrimaryColumns("id")
            .OnDelete(System.Data.Rule.Cascade);
    }
}
