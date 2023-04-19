using FluentMigrator;

namespace DatabaseMigrator.Migrations.Resources
{
    [Migration(202304172134)]
    public class Create_InternalCommands : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("internal_commands")
                .InSchema("resources")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("enqueue_date").AsDateTime2().NotNullable()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("data").AsString().NotNullable()
                .WithColumn("processed_date").AsDateTime2().Nullable()
                .WithColumn("error").AsString().Nullable();
        }
    }
}
