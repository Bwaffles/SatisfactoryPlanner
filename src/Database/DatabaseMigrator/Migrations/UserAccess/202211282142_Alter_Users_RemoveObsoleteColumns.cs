using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess
{
    [Migration(202211282142)]
    public class Alter_Users_RemoveObsoleteColumns : Migration
    {
        public override void Down()
        {
            Alter.Table("users")
                .InSchema("users")
                .AddColumn("username").AsString(100).NotNullable()
                .AddColumn("email").AsString(254).NotNullable().WithDefaultValue("test@test.com")
                .AddColumn("password").AsString(255).NotNullable().WithDefaultValue("fake password")
                .AddColumn("is_active").AsBoolean().NotNullable().WithDefaultValue(false);
        }

        public override void Up()
        {
            Delete
                .Column("username")
                .Column("email")
                .Column("password")
                .Column("is_active")
                .FromTable("users").InSchema("users");
        }
    }
}
