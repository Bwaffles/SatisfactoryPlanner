using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess
{
    [Migration(202205091004)]
    public class Create_Users : Migration
    {
        public override void Down()
        {
            Delete
                .Table("users")
                .InSchema("users");

            Delete
                .Schema("users");
        }

        public override void Up()
        {
            Create.Schema("users");

            Create.Table("users")
                .InSchema("users")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("login").AsString(100).NotNullable()
                .WithColumn("email").AsString(254).NotNullable()
                .WithColumn("password").AsString(255).NotNullable()
                .WithColumn("is_active").AsBoolean().NotNullable().WithDefaultValue(false);
        }
    }
}
