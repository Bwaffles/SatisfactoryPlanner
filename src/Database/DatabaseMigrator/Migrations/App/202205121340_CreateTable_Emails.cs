using FluentMigrator;

namespace DatabaseMigrator.Migrations.App
{
    [Migration(202205121340)]
    public class CreateTable_Emails : Migration
    {
        public override void Down()
        {
            Delete
                .Table("emails")
                .InSchema("app");
        }

        public override void Up()
        {
            Create.Table("emails")
                .InSchema("app")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("from").AsString().NotNullable()
                .WithColumn("to").AsString().NotNullable()
                .WithColumn("subject").AsString().NotNullable()
                .WithColumn("content").AsString().NotNullable()
                .WithColumn("date").AsDateTime2().NotNullable();
        }
    }
}
