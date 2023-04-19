using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess.Permissions
{
    [Migration(202304170926)]
    public class AddPermission_GetExtractors : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("permissions").InSchema("users").Row(new
            {
                code = "Resources.GetExtractors"
            });

            Insert.IntoTable("role_permissions").InSchema("users").Row(new
            {
                role_code = "Pioneer",
                permission_code = "Resources.GetExtractors"
            });
        }
    }
}
