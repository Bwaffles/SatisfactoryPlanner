using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess.Permissions
{
    [Migration(202302212396)]
    public class AddPermission_GetNodeDetails : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("permissions").InSchema("users").Row(new
            {
                code = "Resources.GetNodeDetails"
            });

            Insert.IntoTable("role_permissions").InSchema("users").Row(new
            {
                role_code = "Pioneer",
                permission_code = "Resources.GetNodeDetails"
            });
        }
    }
}
