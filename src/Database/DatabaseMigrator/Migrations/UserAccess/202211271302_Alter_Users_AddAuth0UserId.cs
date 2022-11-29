using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess
{
    [Migration(202211271302)]
    public class Alter_Users_AddAuth0UserId : Migration
    {
        public override void Down()
        {
            Delete.Column("auth0_user_id").FromTable("users").InSchema("users");
        }

        public override void Up()
        {
            Alter.Table("users")
                .InSchema("users")
                .AddColumn("auth0_user_id").AsString().NotNullable().Unique().WithColumnDescription("The identifier of the user in Auth0.");
        }
    }
}
