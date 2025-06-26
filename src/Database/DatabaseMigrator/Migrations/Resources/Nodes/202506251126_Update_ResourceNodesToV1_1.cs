using DatabaseMigrator.Scripts.Generators._0005_update_resource_nodes_v1._1;
using FluentMigrator;

namespace DatabaseMigrator.Migrations.Resources.Nodes
{
    /// <summary>
    ///     Update the nodes to v1.1.
    /// </summary>
    [Migration(202506251126)]
    public class Update_ResourceNodesToV1_1 : Migration
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

            Execute.Script(Scripts.Scripts.UpdateResourceNodes1_1);
        }
    }
}