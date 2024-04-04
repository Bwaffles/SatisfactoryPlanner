using FluentMigrator;

namespace DatabaseMigrator.Migrations.Production
{
    [Migration(202204151110)]
    public class Alter_Items_ToIdPrimaryKey : Migration
    {
        public override void Down()
        {
            // Restore item_code column on resource_extractor_allowed_resources
            Create
                   .Column("item_code")
                   .OnTable("resource_extractor_allowed_resources")
                   .InSchema("factories")
                       .AsString()
                       .Nullable();

            Execute.Sql("UPDATE factories.resource_extractor_allowed_resources" +
                        "   SET item_code = (SELECT item.code " +
                                              "FROM factories.items AS item " +
                                             "WHERE item.id = item_id)");

            Delete
                .Column("item_id")
                .FromTable("resource_extractor_allowed_resources")
                .InSchema("factories");

            // Restore item_code column on resource_nodes
            Create
                   .Column("item_code")
                   .OnTable("resource_nodes")
                   .InSchema("factories")
                       .AsString()
                       .Nullable();

            Execute.Sql("UPDATE factories.resource_nodes" +
                        "   SET item_code = (SELECT item.code " +
                                              "FROM factories.items AS item " +
                                             "WHERE item.id = item_id)");

            Delete
                .Column("item_id")
                .FromTable("resource_nodes")
                .InSchema("factories");

            //
            Delete
                .UniqueConstraint()
                .FromTable("items")
                .InSchema("factories")
                .Column("code");

            Delete
                .Column("id")
                .FromTable("items")
                .InSchema("factories");

            Alter
                .Column("code")
                .OnTable("items")
                .InSchema("factories")
                    .AsString();

            Create
                .PrimaryKey("PK_items")
                .OnTable("items")
                .WithSchema("factories")
                .Column("code");

            Alter
                .Column("item_code")
                .OnTable("resource_extractor_allowed_resources")
                .InSchema("factories")
                    .AsString()
                    .NotNullable()
                    .ForeignKey("", "factories", "items", "code");

            Alter
                .Column("item_code")
                .OnTable("resource_nodes")
                .InSchema("factories")
                    .AsString()
                    .NotNullable()
                    .ForeignKey("", "factories", "items", "code");
        }

        public override void Up()
        {
            Create
                .Column("id")
                .OnTable("items")
                .InSchema("factories")
                    .AsGuid()
                    .NotNullable()
                    .WithDefault(SystemMethods.NewGuid);

            // Move data from item_code to new item_id column for resource_extractor_allowed_resources
            Create
                .Column("item_id")
                .OnTable("resource_extractor_allowed_resources")
                .InSchema("factories")
                    .AsGuid()
                    .Nullable();

            Execute.Sql("UPDATE factories.resource_extractor_allowed_resources" +
                        "   SET item_id = (SELECT item.id " +
                                            "FROM factories.items AS item " +
                                           "WHERE item.code = item_code)");

            Delete
                .Column("item_code")
                .FromTable("resource_extractor_allowed_resources")
                .InSchema("factories");

            // Move data from item_code to new item_id column for resource_nodes
            Create
                .Column("item_id")
                .OnTable("resource_nodes")
                .InSchema("factories")
                    .AsGuid()
                    .Nullable();

            Execute.Sql("UPDATE factories.resource_nodes" +
                        "   SET item_id = (SELECT item.id " +
                                            "FROM factories.items AS item " +
                                           "WHERE item.code = item_code)");

            Delete
                .Column("item_code")
                .FromTable("resource_nodes")
                .InSchema("factories");

            // Fix primary key on items table to be id columns
            Delete
                .PrimaryKey("PK_items")
                .FromTable("items")
                .InSchema("factories");

            Create
                .PrimaryKey("PK_items")
                .OnTable("items")
                .WithSchema("factories")
                .Column("id");

            // Add back the FK to the new item.id column
            Create
                .ForeignKey()
                .FromTable("resource_extractor_allowed_resources").InSchema("factories").ForeignColumn("item_id")
                .ToTable("items").InSchema("factories").PrimaryColumn("id");

            Create
                .ForeignKey()
                .FromTable("resource_nodes").InSchema("factories").ForeignColumn("item_id")
                .ToTable("items").InSchema("factories").PrimaryColumn("id");

            // Add a unique constraint on the code column just so we don't have duplicate items from the game
            Create
                .UniqueConstraint()
                .OnTable("items")
                .WithSchema("factories")
                .Column("code");

            Alter
                .Table("items")
                .InSchema("factories")
                .AlterColumn("code")
                    .AsString()
                    .NotNullable()
                    .WithColumnDescription("The code that maps to the ClassName of the item in the game data files.");
        }
    }
}
