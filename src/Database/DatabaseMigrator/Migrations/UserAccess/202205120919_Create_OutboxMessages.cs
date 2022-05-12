using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess
{
    [Migration(202205120919)]
    public class Create_OutboxMessages : Migration
    {
        public override void Down()
        {
            Delete
                .Table("outbox_messages")
                .InSchema("users");
        }

        public override void Up()
        {
            Create.Table("outbox_messages")
                .InSchema("users")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("occurred_on").AsDateTime2().NotNullable()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("data").AsString().NotNullable()
                .WithColumn("processed_date").AsDateTime2().Nullable();
        }
    }
}
