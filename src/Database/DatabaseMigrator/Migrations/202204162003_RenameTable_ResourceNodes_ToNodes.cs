using FluentMigrator;

namespace DatabaseMigrator.Migrations
{
    [Migration(202204162003)]
    public class RenameTable_ResourceNodes_ToNodes : Migration
    {
        public override void Down()
        {
            Rename
                .Column("node_id")
                .OnTable("resource_node_extractions")
                .InSchema("resources")
                .To("resource_node_id");

            Rename
                .Table("nodes")
                .InSchema("resources")
                .To("resource_nodes")
                .InSchema("resources");
        }

        public override void Up()
        {
            Rename
                .Table("resource_nodes")
                .InSchema("resources")
                .To("nodes")
                .InSchema("resources");

            Rename
                .Column("resource_node_id")
                .OnTable("resource_node_extractions")
                .InSchema("resources")
                .To("node_id");
        }
    }
}
