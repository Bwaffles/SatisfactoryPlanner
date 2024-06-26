using FluentMigrator;

namespace DatabaseMigrator.Migrations.Warehouses.ItemSources;

[Migration(202406211037)]
public class Create_ItemSources : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("item_sources")
            .InSchema("warehouses")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("world_id").AsGuid().NotNullable()
            .WithColumn("source_id").AsGuid().NotNullable()
            .WithColumn("source_name").AsString().NotNullable()
            .WithColumn("source_type").AsString().NotNullable()
            ;
    }
}
