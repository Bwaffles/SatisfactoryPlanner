using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess
{
    [Migration(202205121056)]
    public class Create_InboxMessages : Migration
    {
        public override void Down()
        {
            Delete
                .Table("inbox_messages")
                .InSchema("users");
        }

        public override void Up()
        {
            Create.Table("inbox_messages")
                .InSchema("users")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("occurred_on").AsDateTime2().NotNullable()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("data").AsString().NotNullable()
                .WithColumn("processed_date").AsDateTime2().Nullable();
        }
    }
}
