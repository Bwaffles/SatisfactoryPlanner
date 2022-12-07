using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess
{
    [Migration(202212071749)]
    public class Delete_UserRegistrations : Migration
    {
        public override void Down()
        {
            Create.Table("user_registrations")
                .InSchema("users")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("login").AsString(100).NotNullable()
                .WithColumn("email").AsString(254).NotNullable()
                .WithColumn("password").AsString(255).NotNullable()
                .WithColumn("status_code").AsString(50).NotNullable()
                .WithColumn("register_date").AsDateTime().NotNullable()
                .WithColumn("confirmed_date").AsDateTime().Nullable();
        }

        public override void Up()
        {
            Delete
                .Table("user_registrations")
                .InSchema("users");
        }
    }
}
