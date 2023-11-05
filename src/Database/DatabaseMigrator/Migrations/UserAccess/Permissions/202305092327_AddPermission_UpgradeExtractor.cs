using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess.Permissions
{
    [Migration(202305092327)]
    public class AddPermission_UpgradeExtractor : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("permissions").InSchema("users").Row(new
            {
                code = "Resources.UpgradeExtractor"
            });

            Insert.IntoTable("role_permissions").InSchema("users").Row(new
            {
                role_code = "Pioneer",
                permission_code = "Resources.UpgradeExtractor"
            });
        }
    }
}
