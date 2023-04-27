using FluentMigrator;

namespace DatabaseMigrator.Migrations.Resources.WorldNodes
{
    [Migration(202304222320)]
    public class RenameTable_FromTappedNodes_ToWorldNodes : AutoReversingMigration
    {
        public override void Up()
        {
            Rename.Table("tapped_nodes")
                .InSchema("resources")
                .To("world_nodes")
                .InSchema("resources");
        }
    }
}