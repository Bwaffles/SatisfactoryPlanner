using FluentMigrator;

namespace DatabaseMigrator.Migrations.Production
{
    [Migration(202204091730)]
    public class Update_ItemForm_ToForeignKey : Migration
    {
        public override void Down()
        {
            Alter
                .Table("items")
                .InSchema("factories")
                .AddColumn("form")
                    .AsString()
                    .Nullable()
                    .WithColumnDescription("The form this item is in, i.e. does it require pipes or conveyors, can the player pick it up etc.");

            Execute.Sql("UPDATE factories.items" +
                        "   SET form = (SELECT resource_form.name " +
                                         "FROM factories.resource_forms resource_form " +
                                        "WHERE resource_form.code = resource_form)");

            Alter
                .Column("form")
                .OnTable("items")
                .InSchema("factories")
                    .AsString()
                    .NotNullable();

            Execute.Sql("ALTER TABLE factories.items " +
                        "ADD CONSTRAINT form_check CHECK (form in ('Solid', " +
                                                                  "'Liquid', " +
                                                                  "'Gas')" +
                                                        ");");

            Delete
                .Column("resource_form")
                .FromTable("items")
                .InSchema("factories");

            Delete
                .Table("resource_forms")
                .InSchema("factories");
        }

        public override void Up()
        {
            // Add the new lookup table so we can keep codes in the tables for easier cross checking with the game files
            Create
                .Table("resource_forms")
                .WithDescription("The form an item is in, i.e. does it require pipes or conveyors, can the player pick it up etc.")
                .InSchema("factories")
                .WithColumn("code")
                    .AsString()
                    .PrimaryKey()
                .WithColumn("name")
                    .AsString()
                    .NotNullable();

            Insert
                .IntoTable("resource_forms")
                .InSchema("factories")
                .Row(new { code = "RF_SOLID", name = "Solid" })
                .Row(new { code = "RF_LIQUID", name = "Liquid" })
                .Row(new { code = "RF_GAS", name = "Gas" });

            // Add new column as nullable since it'll start empty
            Alter
                .Table("items")
                .InSchema("factories")
                .AddColumn("resource_form")
                    .AsString()
                    .Nullable()
                    .ForeignKey("", "factories", "resource_forms", "code");

            // Convert the name from the form column into the code in the resource_form column
            Execute.Sql("UPDATE factories.items" +
                        "   SET resource_form = (SELECT resource_form.code " +
                                                  "FROM factories.resource_forms AS resource_form " +
                                                 "WHERE resource_form.name = form)");

            // Alter the column to be not nullable finally
            Alter
                .Column("resource_form")
                .OnTable("items")
                .InSchema("factories")
                    .AsString()
                    .NotNullable();

            // Clean up the old form column
            Delete
                .Column("form")
                .FromTable("items")
                .InSchema("factories");
        }
    }
}
