using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess
{
    [Migration(202205121030)]
    public class Create_InternalCommands : Migration
    {
        public override void Down()
        {
            Delete
                .Table("internal_commands")
                .InSchema("users");
        }

        public override void Up()
        {
            Create.Table("internal_commands")
                .InSchema("users")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("enqueue_date").AsDateTime2().NotNullable()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("data").AsString().NotNullable()
                .WithColumn("processed_date").AsDateTime2().Nullable()
                .WithColumn("error").AsString().Nullable();
        }
    }
}
