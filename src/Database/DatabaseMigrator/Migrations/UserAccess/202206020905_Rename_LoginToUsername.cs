using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess
{
    [Migration(202206020905)]
    public class Rename_LoginToUsername : Migration
    {
        public override void Down()
        {
            Rename
                .Column("username")
                .OnTable("users")
                .InSchema("users")
                .To("login");

            Rename
                .Column("username")
                .OnTable("user_registrations")
                .InSchema("users")
                .To("login");
        }

        public override void Up()
        {
            Rename
                .Column("login")
                .OnTable("users")
                .InSchema("users")
                .To("username");

            Rename
                .Column("login")
                .OnTable("user_registrations")
                .InSchema("users")
                .To("username");
        }
    }
}
