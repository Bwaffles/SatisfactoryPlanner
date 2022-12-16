using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess
{
    [Migration(202212151232)]
    public class Create_UserRoles : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("user_roles")
                .InSchema("users")
                .WithColumn("user_id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("role_code").AsString().NotNullable().PrimaryKey();
        }
    }
}
