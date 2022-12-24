using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess.Permissions
{
    [Migration(202212152348)]
    public class Create_Permissions : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("permissions")
                .InSchema("users")
                .WithColumn("code").AsString(100).NotNullable().PrimaryKey();

            Insert.IntoTable("permissions").InSchema("users").Row(new
            {
                code = "Resources.GetResources"
            });

            Create.Table("role_permissions")
                .InSchema("users")
                .WithColumn("role_code").AsString().NotNullable().PrimaryKey()
                .WithColumn("permission_code").AsString(100).NotNullable().PrimaryKey();

            Insert.IntoTable("role_permissions").InSchema("users").Row(new
            {
                role_code = "Pioneer",
                permission_code = "Resources.GetResources"
            });
        }
    }
}
