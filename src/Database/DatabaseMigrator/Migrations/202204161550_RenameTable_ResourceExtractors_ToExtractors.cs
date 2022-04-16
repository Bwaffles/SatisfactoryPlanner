using FluentMigrator;

namespace DatabaseMigrator.Migrations
{
    [Migration(202204161550)]
    public class RenameTable_ResourceExtractors_ToExtractors : Migration
    {
        public override void Down()
        {
            Delete
                .PrimaryKey("PK_extractor_allowed_resources")
                .FromTable("extractor_allowed_resources")
                .InSchema("resources");

            Create
                .Column("id")
                .OnTable("extractor_allowed_resources")
                .InSchema("resources")
                    .AsGuid()
                    .NotNullable()
                    .PrimaryKey("PK_extractor_allowed_resources")
                    .WithDefault(SystemMethods.NewGuid);

            Rename
                .Table("extractor_allowed_resources")
                .InSchema("resources")
                .To("resource_extractor_allowed_resources");

            Rename
                .Column("extractor_id")
                .OnTable("resource_extractor_allowed_resources")
                .InSchema("resources")
                .To("resource_extractor_id");

            Rename
                .Column("extractor_id")
                .OnTable("resource_node_extractions")
                .InSchema("resources")
                .To("resource_extractor_id");

            Rename
                .Table("extractors")
                .InSchema("resources")
                .To("resource_extractors")
                .InSchema("resources");
        }

        public override void Up()
        {
            Rename
                .Table("resource_extractors")
                .InSchema("resources")
                .To("extractors")
                .InSchema("resources");

            Rename
                .Column("resource_extractor_id")
                .OnTable("resource_node_extractions")
                .InSchema("resources")
                .To("extractor_id");

            Rename
                .Column("resource_extractor_id")
                .OnTable("resource_extractor_allowed_resources")
                .InSchema("resources")
                .To("extractor_id");

            Rename
                .Table("resource_extractor_allowed_resources")
                .InSchema("resources")
                .To("extractor_allowed_resources");

            Delete
                .Column("id")
                .FromTable("extractor_allowed_resources")
                .InSchema("resources");

            Create
                .PrimaryKey("PK_extractor_allowed_resources")
                .OnTable("extractor_allowed_resources")
                .WithSchema("resources")
                .Columns("extractor_id", "resource_id");
        }
    }
}
