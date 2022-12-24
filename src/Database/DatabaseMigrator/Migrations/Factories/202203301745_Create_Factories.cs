using FluentMigrator;

namespace DatabaseMigrator.Migrations.Factories
{
    [Migration(202203301745)]
    public class Create_Factories : Migration
    {
        public override void Down()
        {
            Delete.Schema("factories");

            Delete.Table("factories");
        }

        public override void Up()
        {
            Execute.Sql($"CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");

            Create.Schema("factories");

            Create.Table("factories")
                .InSchema("factories")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("name").AsString().NotNullable();
        }
    }
}
