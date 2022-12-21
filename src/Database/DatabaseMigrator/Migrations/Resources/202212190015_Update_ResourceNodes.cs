using DatabaseMigrator.Scripts.Generators._0004_UpdateResourceNodes;
using FluentMigrator;

namespace DatabaseMigrator.Migrations.Resources
{
    /// <summary>
    ///     Update the nodes to update 7. Major change is the movement of the oil nodes from the spire coast.
    ///     Other notable change is the 5 bauxite nodes in the titan forest, and the uranium node in the rocky desert
    ///     were (probably accidentally) deleted then re-added under a new instance_name.
    /// </summary>
    [Migration(202212190015)]
    public class Update_ResourceNodes : Migration
    {
        public override void Down()
        {
            //Delete.Table("temp_nodes").InSchema("resources");
        }

        public override void Up()
        {
            // Tested this with a temp table
            //Execute.Sql("create table resources.temp_nodes as (select * from resources.nodes);");

            new UpdateResourceNodesScriptGenerator().Generate("nodes");

            Execute.Script(Scripts.Scripts.UpdateResourceNodes);
        }
    }
}