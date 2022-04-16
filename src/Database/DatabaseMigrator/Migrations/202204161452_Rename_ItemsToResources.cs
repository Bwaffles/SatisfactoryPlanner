using FluentMigrator;

namespace DatabaseMigrator.Migrations
{
    [Migration(202204161452)]
    public class Rename_ItemsToResources : Migration
    {
        public override void Down()
        {
            Create
                .Column("can_be_deleted")
                .OnTable("resources")
                .InSchema("resources")
                    .AsBoolean()
                    .NotNullable()
                    .WithColumnDescription("Whether the item can be deleted by the pioneer.")
                    .WithDefaultValue(true);

            Create
                .Column("type")
                .OnTable("resources")
                .InSchema("resources")
                    .AsString()
                    .NotNullable()
                    .WithDefaultValue("Resource");

            Rename
                .Column("resource_id")
                .OnTable("resource_nodes")
                .InSchema("resources")
                .To("item_id");

            Rename
                .Column("resource_id")
                .OnTable("resource_extractor_allowed_resources")
                .InSchema("resources")
                .To("item_id");

            Rename
                .Table("resources")
                .InSchema("resources")
                .To("items")
                .InSchema("resources");
        }

        public override void Up()
        {
            Rename
                .Table("items")
                .InSchema("resources")
                .To("resources")
                .InSchema("resources");

            Rename
                .Column("item_id")
                .OnTable("resource_nodes")
                .InSchema("resources")
                .To("resource_id");

            Rename
                .Column("item_id")
                .OnTable("resource_extractor_allowed_resources")
                .InSchema("resources")
                .To("resource_id");
            
            // Remove some columns that don't make sense in a resource context
            // Not removing all of them because it'd be hard to revert data and I don't want to deal with that now
            Delete
                .Column("type")
                .FromTable("resources")
                .InSchema("resources");

            Delete
                .Column("can_be_deleted")
                .FromTable("resources")
                .InSchema("resources");
        }
    }
}
