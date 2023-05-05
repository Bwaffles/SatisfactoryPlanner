using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess.Permissions
{
    [Migration(202305032237)]
    public class AddPermission_DecreaseWorldNodeExtractionRate : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("permissions").InSchema("users").Row(new
            {
                code = "Resources.DecreaseWorldNodeExtractionRate"
            });

            Insert.IntoTable("role_permissions").InSchema("users").Row(new
            {
                role_code = "Pioneer",
                permission_code = "Resources.DecreaseWorldNodeExtractionRate"
            });
        }
    }
}
