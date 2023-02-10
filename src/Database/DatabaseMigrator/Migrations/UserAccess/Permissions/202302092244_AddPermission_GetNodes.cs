using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess.Permissions
{
    [Migration(202302092244)]
    public class AddPermission_GetNodes : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("permissions").InSchema("users").Row(new
            {
                code = "Resources.GetNodes"
            });

            Insert.IntoTable("role_permissions").InSchema("users").Row(new
            {
                role_code = "Pioneer",
                permission_code = "Resources.GetNodes"
            });
        }
    }
}
