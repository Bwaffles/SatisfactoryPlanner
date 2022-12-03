using FluentMigrator;

namespace DatabaseMigrator.Migrations.Pioneers
{
    [Migration(202212021201)]
    public class Create_InternalCommands : Migration
    {
        public override void Down()
        {
            Delete
                .Table("internal_commands")
                .InSchema("pioneers");
        }

        public override void Up()
        {
            Create.Table("internal_commands")
                .InSchema("pioneers")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("enqueue_date").AsDateTime2().NotNullable()
                .WithColumn("type").AsString().NotNullable()
                .WithColumn("data").AsString().NotNullable()
                .WithColumn("processed_date").AsDateTime2().Nullable()
                .WithColumn("error").AsString().Nullable();
        }
    }
}
