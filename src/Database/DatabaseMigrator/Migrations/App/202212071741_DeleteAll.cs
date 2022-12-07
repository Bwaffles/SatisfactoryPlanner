using FluentMigrator;

namespace DatabaseMigrator.Migrations.App
{
    [Migration(202212071741)]
    public class DeleteAll : Migration
    {
        public override void Down()
        {
            Create
                .Schema("app");

            Create.Table("emails")
                .InSchema("app")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("from").AsString().NotNullable()
                .WithColumn("to").AsString().NotNullable()
                .WithColumn("subject").AsString().NotNullable()
                .WithColumn("content").AsString().NotNullable()
                .WithColumn("date").AsDateTime2().NotNullable();
        }

        public override void Up()
        {
            Delete
                .Table("emails")
                .InSchema("app");

            Delete
                .Schema("app");
        }
    }
}
