using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess.Permissions
{
    [Migration(202304171536)]
    public class DeletePermission_GetExtractors : Migration
    {
        public override void Up()
        {
            Delete.FromTable("permissions").InSchema("users").Row(new
            {
                code = "Resources.GetExtractors"
            });

            Delete.FromTable("role_permissions").InSchema("users").Row(new
            {
                role_code = "Pioneer",
                permission_code = "Resources.GetExtractors"
            });
        }

        public override void Down()
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
