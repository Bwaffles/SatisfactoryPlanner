using DatabaseMigrator.Scripts.Generators.SeedResourceExtractors;
using FluentMigrator;

namespace DatabaseMigrator.Migrations.Factories
{
    [Migration(202204101100)]
    public class Create_ResourceExtractors_Seeded : Migration
    {
        public override void Down()
        {
            new SeedResourceExtractorsScriptGenerator()
                .Delete();

            Delete
                .Table("resource_extractor_allowed_resources")
                .InSchema("factories");

            Delete
                .Table("resource_extractors")
                .InSchema("factories");
        }

        public override void Up()
        {
            Create
                .Table("resource_extractors")
                .InSchema("factories")
                .WithColumn("code")
                    .AsString()
                    .PrimaryKey()
                .WithColumn("name")
                    .AsString()
                    .NotNullable()
                .WithColumn("description")
                    .AsString()
                    .NotNullable()
                .WithColumn("extract_cycle_time")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("How many seconds it takes to perform 1 cycle of extraction.")
                .WithColumn("items_per_cycle")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("How many items are extracted each cycle of operation.")
                .WithColumn("power_consumption")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("How much power this extractor takes to operate in megawatts (MW).")
                .WithColumn("power_consumption_exponent")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("The rate at which power consumption changes based on the clockspeed.")
                .WithColumn("min_clockspeed")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("The lowest the clockspeed can be set as a percentage e.g. 0.01 = 1%.")
                .WithColumn("max_clockspeed")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("The highest the clockspeed can be set without the addition of power shares as a percentage e.g. 1.00 = 100%.")
                .WithColumn("max_clockspeed_per_shard")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("How much potential clockspeed is added per power shard as a percentage e.g. 0.50 = 50%.")
                .WithColumn("max_shards")
                    .AsInt16()
                    .NotNullable()
                    .WithColumnDescription("How many shards can be placed in the extractor for overclocking.");

            new SeedResourceExtractorsScriptGenerator()
                .Generate();

            Execute.Script(Scripts.Scripts.SeedResourceExtractors);

            Create
                .Table("resource_extractor_allowed_resources")
                .InSchema("factories")
                .WithColumn("id")
                    .AsGuid()
                    .NotNullable()
                    .PrimaryKey()
                    .WithDefault(SystemMethods.NewGuid)
                .WithColumn("resource_extractor_code")
                    .AsString()
                    .ForeignKey("", "factories", "resource_extractors", "code")
                .WithColumn("item_code")
                    .AsString()
                    .ForeignKey("", "factories", "items", "code");

            Insert
                .IntoTable("resource_extractor_allowed_resources")
                .InSchema("factories")
                .Row(new { resource_extractor_code = "Build_OilPump_C", item_code = "Desc_LiquidOil_C" })

                .Row(new { resource_extractor_code = "Build_MinerMk1_C", item_code = "Desc_OreIron_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk1_C", item_code = "Desc_Stone_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk1_C", item_code = "Desc_OreCopper_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk1_C", item_code = "Desc_Coal_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk1_C", item_code = "Desc_OreGold_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk1_C", item_code = "Desc_OreBauxite_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk1_C", item_code = "Desc_Sulfur_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk1_C", item_code = "Desc_RawQuartz_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk1_C", item_code = "Desc_OreUranium_C" })

                .Row(new { resource_extractor_code = "Build_MinerMk2_C", item_code = "Desc_OreIron_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk2_C", item_code = "Desc_Stone_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk2_C", item_code = "Desc_OreCopper_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk2_C", item_code = "Desc_Coal_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk2_C", item_code = "Desc_OreGold_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk2_C", item_code = "Desc_OreBauxite_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk2_C", item_code = "Desc_Sulfur_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk2_C", item_code = "Desc_RawQuartz_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk2_C", item_code = "Desc_OreUranium_C" })

                .Row(new { resource_extractor_code = "Build_MinerMk3_C", item_code = "Desc_OreIron_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk3_C", item_code = "Desc_Stone_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk3_C", item_code = "Desc_OreCopper_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk3_C", item_code = "Desc_Coal_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk3_C", item_code = "Desc_OreGold_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk3_C", item_code = "Desc_OreBauxite_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk3_C", item_code = "Desc_Sulfur_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk3_C", item_code = "Desc_RawQuartz_C" })
                .Row(new { resource_extractor_code = "Build_MinerMk3_C", item_code = "Desc_OreUranium_C" });
        }
    }
}
