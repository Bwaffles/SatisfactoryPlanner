using FluentMigrator;

namespace DatabaseMigrator.Migrations.Factories
{
    /// <summary>
    /// Switching the primary key of this table to a Guid id instead of the game's code.
    /// Using a string code to get from a repository breaks consistency, 
    /// and if the game ever changes the code name I am going to have a rough time.
    /// </summary>
    [Migration(202204121502)]
    public class Alter_ResourceExtractors_ToIdPrimaryKey : Migration
    {
        public override void Down()
        {
            Create
                   .Column("resource_extractor_code")
                   .OnTable("resource_extractor_allowed_resources")
                   .InSchema("factories")
                       .AsString()
                       .Nullable();

            Execute.Sql("UPDATE factories.resource_extractor_allowed_resources" +
                        "   SET resource_extractor_code = (SELECT resource_extractor.code " +
                                                            "FROM factories.resource_extractors AS resource_extractor " +
                                                           "WHERE resource_extractor.id = resource_extractor_id)");

            Delete
                .Column("resource_extractor_id")
                .FromTable("resource_extractor_allowed_resources")
                .InSchema("factories");

            Delete
                .UniqueConstraint()
                .FromTable("resource_extractors")
                .InSchema("factories")
                .Column("code");

            Delete
                .Column("id")
                .FromTable("resource_extractors")
                .InSchema("factories");

            Alter
                .Column("code")
                .OnTable("resource_extractors")
                .InSchema("factories")
                    .AsString();

            Create
                .PrimaryKey("PK_resource_extractors")
                .OnTable("resource_extractors")
                .WithSchema("factories")
                .Column("code");

            Alter
                .Column("resource_extractor_code")
                .OnTable("resource_extractor_allowed_resources")
                .InSchema("factories")
                    .AsString()
                    .NotNullable()
                    .ForeignKey("", "factories", "resource_extractors", "code");
        }

        public override void Up()
        {
            Create
                .Column("id")
                .OnTable("resource_extractors")
                .InSchema("factories")
                    .AsGuid()
                    .NotNullable()
                    .WithDefault(SystemMethods.NewGuid);

            Create
                .Column("resource_extractor_id")
                .OnTable("resource_extractor_allowed_resources")
                .InSchema("factories")
                    .AsGuid()
                    .Nullable();

            Execute.Sql("UPDATE factories.resource_extractor_allowed_resources" +
                        "   SET resource_extractor_id = (SELECT resource_extractor.id " +
                                                          "FROM factories.resource_extractors AS resource_extractor " +
                                                         "WHERE resource_extractor.code = resource_extractor_code)");

            Delete
                .Column("resource_extractor_code")
                .FromTable("resource_extractor_allowed_resources")
                .InSchema("factories");

            Delete
                .PrimaryKey("PK_resource_extractors")
                .FromTable("resource_extractors")
                .InSchema("factories");

            Alter
                .Column("id")
                .OnTable("resource_extractors")
                .InSchema("factories")
                    .AsGuid();

            Create
                .PrimaryKey()
                .OnTable("resource_extractors")
                .WithSchema("factories")
                .Column("id");

            Alter
                .Column("resource_extractor_id")
                .OnTable("resource_extractor_allowed_resources")
                .InSchema("factories")
                    .AsGuid()
                    .NotNullable()
                    .ForeignKey("", "factories", "resource_extractors", "id");

            Create
                .UniqueConstraint()
                .OnTable("resource_extractors")
                .WithSchema("factories")
                .Column("code");

            Alter
                .Table("resource_extractors")
                .InSchema("factories")
                .AlterColumn("code")
                    .AsString()
                    .NotNullable()
                    .WithColumnDescription("The code that maps to the ClassName of the resource extractor in the game data files.");
        }
    }
}
