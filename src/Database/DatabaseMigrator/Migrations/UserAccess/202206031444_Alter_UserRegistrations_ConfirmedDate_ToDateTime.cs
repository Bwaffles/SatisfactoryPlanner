using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess
{
    [Migration(202206031444)]
    public class Alter_UserRegistrations_ConfirmedDate_ToDateTime : Migration
    {
        public override void Down()
        {
            Alter.Column("confirmed_date")
                .OnTable("user_registrations")
                .InSchema("users")
                .AsDate().Nullable();
        }

        public override void Up()
        {
            Alter.Column("confirmed_date")
                .OnTable("user_registrations")
                .InSchema("users")
                .AsDateTime().Nullable();
        }
    }
}
