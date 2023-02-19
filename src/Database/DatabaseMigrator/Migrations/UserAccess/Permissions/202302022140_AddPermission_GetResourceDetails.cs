using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess.Permissions
{
    [Migration(202302022140)]
    public class AddPermission_GetResourceDetails : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("permissions").InSchema("users").Row(new
            {
                code = "Resources.GetResourceDetails"
            });

            Insert.IntoTable("role_permissions").InSchema("users").Row(new
            {
                role_code = "Pioneer",
                permission_code = "Resources.GetResourceDetails"
            });
        }
    }
}
