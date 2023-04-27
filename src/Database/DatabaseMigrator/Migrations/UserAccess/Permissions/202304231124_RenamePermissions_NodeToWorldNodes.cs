using FluentMigrator;

namespace DatabaseMigrator.Migrations.UserAccess.Permissions
{
    [Migration(202304231124)]
    public class RenamePermissions_NodeToWorldNodes : Migration
    {
        public override void Up()
        {
            RenamePermission("Resources.GetNodes", "Resources.GetWorldNodes");
            RenamePermission("Resources.GetNodeDetails", "Resources.GetWorldNodeDetails");
            RenamePermission("Resources.TapNode", "Resources.TapWorldNode");
            RenamePermission("Resources.IncreaseNodeExtractionRate", "Resources.IncreaseWorldNodeExtractionRate");
        }

        public override void Down()
        {
            RenamePermission("Resources.GetWorldNodes", "Resources.GetNodes");
            RenamePermission("Resources.GetWorldNodeDetails", "Resources.GetNodeDetails");
            RenamePermission("Resources.TapWorldNode", "Resources.TapNode");
            RenamePermission("Resources.IncreaseWorldNodeExtractionRate", "Resources.IncreaseNodeExtractionRate");
        }

        private void RenamePermission(string from, string to)
        {
            Update.Table("permissions").InSchema("users")
                .Set(new
                {
                    code = to
                })
                .Where(new
                {
                    code = from
                });

            Update.Table("role_permissions").InSchema("users")
                .Set(new
                {
                    permission_code = to
                })
                .Where(new
                {
                    permission_code = from
                });
        }
    }
}