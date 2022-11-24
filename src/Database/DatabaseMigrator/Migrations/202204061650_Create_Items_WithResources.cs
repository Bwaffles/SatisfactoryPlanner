using FluentMigrator;

namespace DatabaseMigrator.Migrations
{
    [Migration(202204061650)]
    public class Create_Items_WithResources : Migration
    {
        public override void Down()
        {
            Delete
                .Table("items")
                .InSchema("factories");
        }

        public override void Up()
        {
            Create
                .Table("items")
                .InSchema("factories")
                .WithColumn("code")
                    .AsString()
                    .PrimaryKey()
                .WithColumn("name")
                    .AsString()
                    .NotNullable()
                .WithColumn("description")
                    .AsString()
                .WithColumn("type")
                    .AsString()
                    .NotNullable()
                .WithColumn("form")
                    .AsString()
                    .NotNullable()
                    .WithColumnDescription("The form this item is in, i.e. does it require pipes or conveyors, can the player pick it up etc.")
                .WithColumn("stack_size")
                    .AsInt32()
                    .Nullable()
                    .WithColumnDescription("The number of items of a certain type we can stack in one inventory slot or null if the item can't be held.")
                .WithColumn("can_be_deleted")
                    .AsBoolean()
                    .NotNullable()
                    .WithColumnDescription("Whether the item can be deleted by the pioneer.")
                .WithColumn("resource_sink_points")
                    .AsInt64()
                    .Nullable()
                    .WithColumnDescription("How many points are generated when this item is put into the resource sink, or null if it can't be sunk.")
                .WithColumn("energy_value")
                    .AsDecimal()
                    .Nullable()
                    .WithColumnDescription("Energy value in MJ for this item if used as fuel or null when it is not used as fuel.")
                .WithColumn("radioactive_decay")
                    .AsDecimal()
                    .Nullable()
                    .WithColumnDescription("How much radiation this item gives out, or null if it's not radioactive. The higher this value is, the more radioactive the item.")
            ;

            Execute.Sql("ALTER TABLE factories.items " +
                        "ADD CONSTRAINT type_check CHECK (type in ('Resource'));");

            Execute.Sql("ALTER TABLE factories.items " +
                        "ADD CONSTRAINT stack_size_check CHECK (stack_size in ('1', " +
                                                                              "'50', " +
                                                                              "'100', " +
                                                                              "'200', " +
                                                                              "'500')" +
                                                              ");");

            Execute.Sql("ALTER TABLE factories.items " +
                        "ADD CONSTRAINT form_check CHECK (form in ('Solid', " +
                                                                  "'Liquid', " +
                                                                  "'Gas')" +
                                                        ");");

            Execute.Script("Scripts/0001__seed_items_with_resources.sql");
        }
    }
}
