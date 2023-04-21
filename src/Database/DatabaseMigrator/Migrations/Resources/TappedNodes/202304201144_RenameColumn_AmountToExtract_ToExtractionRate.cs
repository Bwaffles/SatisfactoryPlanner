using FluentMigrator;

namespace DatabaseMigrator.Migrations.Resources.TappedNodes
{
    [Migration(202304201144)]
    public class RenameColumn_AmountToExtract_ToExtractionRate : Migration
    {
        public override void Up()
        {
            Rename
                .Column("amount_to_extract")
                .OnTable("tapped_nodes")
                .InSchema("resources")
                .To("extraction_rate");

            Alter
                .Column("extraction_rate")
                .OnTable("tapped_nodes")
                .InSchema("resources")
                .AsDecimal()
                .NotNullable()
                .WithColumnDescription("The number of resources being extracted per minute.");
        }

        public override void Down()
        {
            Rename
                .Column("extraction_rate")
                .OnTable("tapped_nodes")
                .InSchema("resources")
                .To("amount_to_extract");

            Alter
                .Column("amount_to_extract")
                .OnTable("tapped_nodes")
                .InSchema("resources")
                .AsDecimal()
                .NotNullable()
                .WithColumnDescription("The amount being extracted.");
        }
    }
}