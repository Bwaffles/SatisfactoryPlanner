using FluentMigrator;

namespace DatabaseMigrator.Migrations.Resources
{
    [Migration(202204161440)]
    public class CreateSchema_Resources : Migration
    {
        public override void Down()
        {
            Alter
                .Table("resource_node_extractions")
                .InSchema("resources")
                .ToSchema("factories");

            Alter
                .Table("resource_forms")
                .InSchema("resources")
                .ToSchema("factories");

            Alter
                .Table("resource_extractors")
                .InSchema("resources")
                .ToSchema("factories");

            Alter
                .Table("resource_extractor_allowed_resources")
                .InSchema("resources")
                .ToSchema("factories");

            Alter
                .Table("resource_nodes")
                .InSchema("resources")
                .ToSchema("factories");

            Alter
                .Table("items")
                .InSchema("resources")
                .ToSchema("factories");

            Delete
                .Schema("resources");
        }

        public override void Up()
        {
            Create
                .Schema("resources");

            Alter
                .Table("resource_node_extractions")
                .InSchema("factories")
                .ToSchema("resources");

            Alter
                .Table("resource_forms")
                .InSchema("factories")
                .ToSchema("resources");

            Alter
                .Table("resource_extractors")
                .InSchema("factories")
                .ToSchema("resources");

            Alter
                .Table("resource_extractor_allowed_resources")
                .InSchema("factories")
                .ToSchema("resources");

            Alter
                .Table("resource_nodes")
                .InSchema("factories")
                .ToSchema("resources");

            Alter
                .Table("items")
                .InSchema("factories")
                .ToSchema("resources");
        }
    }
}
