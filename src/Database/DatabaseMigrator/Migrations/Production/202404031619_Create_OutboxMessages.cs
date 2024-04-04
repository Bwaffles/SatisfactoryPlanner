using FluentMigrator;

namespace DatabaseMigrator.Migrations.Production
{
    [Migration(202404031619)]
    public class Create_OutboxMessages : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("outbox_messages")
                .InSchema("production")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("occurred_on").AsDateTime2().NotNullable()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("data").AsString().NotNullable()
                .WithColumn("processed_date").AsDateTime2().Nullable();
        }
    }
}
