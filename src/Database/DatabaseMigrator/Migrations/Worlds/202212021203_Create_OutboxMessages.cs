using FluentMigrator;

namespace DatabaseMigrator.Migrations.Worlds
{
    [Migration(202212021203)]
    public class Create_OutboxMessages : Migration
    {
        public override void Down()
        {
            Delete
                .Table("outbox_messages")
                .InSchema("pioneers");
        }

        public override void Up()
        {
            Create.Table("outbox_messages")
                .InSchema("pioneers")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("occurred_on").AsDateTime2().NotNullable()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("data").AsString().NotNullable()
                .WithColumn("processed_date").AsDateTime2().Nullable();
        }
    }
}
