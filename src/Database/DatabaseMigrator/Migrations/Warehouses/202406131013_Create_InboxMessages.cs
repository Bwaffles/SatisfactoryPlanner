using FluentMigrator;

namespace DatabaseMigrator.Migrations.Warehouses
{
    [Migration(202406131013)]
    public class Create_InboxMessages : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("inbox_messages")
                .InSchema("warehouses")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("occurred_on").AsDateTime2().NotNullable()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("data").AsString().NotNullable()
                .WithColumn("processed_date").AsDateTime2().Nullable();
        }
    }
}
