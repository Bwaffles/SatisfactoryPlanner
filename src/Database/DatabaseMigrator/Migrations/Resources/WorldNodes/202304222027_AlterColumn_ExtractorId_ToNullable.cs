using FluentMigrator;

namespace DatabaseMigrator.Migrations.Resources.WorldNodes
{
    [Migration(202304222027)]
    public class AlterColumn_ExtractorId_ToNullable : Migration
    {
        public override void Up()
        {
            Delete.ForeignKey("FK_resource_node_extractions_resource_extractor_id_resource_ext")
                .OnTable("tapped_nodes")
                .InSchema("resources");

            Alter
                .Column("extractor_id")
                .OnTable("tapped_nodes")
                .InSchema("resources")
                .AsGuid()
                .Nullable();
        }

        public override void Down()
        {
            Alter
                .Column("extractor_id")
                .OnTable("tapped_nodes")
                .InSchema("resources")
                .AsGuid()
                .NotNullable()
                .ForeignKey("FK_resource_node_extractions_resource_extractor_id_resource_ext", "resources", "extractors", "id");
        }
    }
}