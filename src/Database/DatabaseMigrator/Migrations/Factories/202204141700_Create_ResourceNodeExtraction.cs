using FluentMigrator;

namespace DatabaseMigrator.Migrations.Factories
{
    [Migration(202204141700)]
    public class Create_ResourceNodeExtraction : Migration
    {
        public override void Down()
        {
            Delete
                .Table("resource_node_extractions")
                .InSchema("factories");
        }

        public override void Up()
        {
            Create
                .Table("resource_node_extractions")
                .InSchema("factories")
                .WithColumn("id")
                    .AsGuid()
                    .PrimaryKey()
                    .WithDefault(SystemMethods.NewGuid)
                .WithColumn("resource_node_id")
                    .AsGuid()
                    .NotNullable()
                    .ForeignKey("", "factories", "resource_nodes", "id")
                .WithColumn("resource_extractor_id")
                    .AsGuid()
                    .NotNullable()
                    .ForeignKey("", "factories", "resource_extractors", "id")
                .WithColumn("amount")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("The amount being extracted.")
                .WithColumn("name")
                    .AsString()
                    .Nullable()
                    .WithColumnDescription("A name to identify this extraction (e.g. Bauxite - Red Forest 1).");
        }
    }
}
