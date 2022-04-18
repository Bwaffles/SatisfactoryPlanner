using FluentMigrator;

namespace DatabaseMigrator.Migrations
{
    [Migration(202204180115)]
    public class AlterColumns_Extractors : Migration
    {
        public override void Down()
        {
            Rename
                .Column("seconds_to_complete_cycle")
                .OnTable("extractors")
                .InSchema("resources")
                .To("extract_cycle_time");

            Rename
                .Column("resources_extracted_per_cycle")
                .OnTable("extractors")
                .InSchema("resources")
                .To("items_per_cycle");

            Alter
                .Column("items_per_cycle")
                .OnTable("extractors")
                .InSchema("resources")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("How many items are extracted each cycle of operation.");

            Rename
                .Column("default_clockspeed")
                .OnTable("extractors")
                .InSchema("resources")
                .To("max_clockspeed");

            Alter
                .Column("max_clockspeed")
                .OnTable("extractors")
                .InSchema("resources")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("The highest the clockspeed can be set without the addition of power shares as a percentage e.g. 1.00 = 100%.");

            Rename
                .Column("overclock_per_shard")
                .OnTable("extractors")
                .InSchema("resources")
                .To("max_clockspeed_per_shard");

            Alter
                .Column("max_clockspeed_per_shard")
                .OnTable("extractors")
                .InSchema("resources")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("How much potential clockspeed is added per power shard as a percentage e.g. 0.50 = 50%.");

            Rename
                .Table("tapped_nodes")
                .InSchema("resources")
                .To("resource_node_extractions");

            Rename
                .Column("amount_to_extract")
                .OnTable("resource_node_extractions")
                .InSchema("resources")
                .To("amount");
        }

        public override void Up()
        {
            Rename
                .Column("extract_cycle_time")
                .OnTable("extractors")
                .InSchema("resources")
                .To("seconds_to_complete_cycle");

            Rename
                .Column("items_per_cycle")
                .OnTable("extractors")
                .InSchema("resources")
                .To("resources_extracted_per_cycle");

            Alter
                .Column("resources_extracted_per_cycle")
                .OnTable("extractors")
                .InSchema("resources")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("How many resources are extracted per cycle.");

            Rename
                .Column("max_clockspeed")
                .OnTable("extractors")
                .InSchema("resources")
                .To("default_clockspeed");

            Alter
                .Column("default_clockspeed")
                .OnTable("extractors")
                .InSchema("resources")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("The highest the clockspeed can be set without the addition of power shards as a percentage e.g. 1.00 = 100%.");

            Rename
                .Column("max_clockspeed_per_shard")
                .OnTable("extractors")
                .InSchema("resources")
                .To("overclock_per_shard");

            Alter
                .Column("overclock_per_shard")
                .OnTable("extractors")
                .InSchema("resources")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("How much the clockspeed increases above the default_clockspeed when a shard is placed in the extractor as a percentage, e.g. 0.5 = 50%.");

            Rename
                .Table("resource_node_extractions")
                .InSchema("resources")
                .To("tapped_nodes");

            Rename
                .Column("amount")
                .OnTable("tapped_nodes")
                .InSchema("resources")
                .To("amount_to_extract");
        }
    }
}
