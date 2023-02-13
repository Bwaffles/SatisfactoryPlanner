using FluentMigrator;

namespace DatabaseMigrator.Migrations.Resources.Nodes
{
    [Migration(202302102326)]
    public class AddColumn_Number : Migration
    {
        public override void Up()
        {
            Create
                .Column("number")
                .OnTable("nodes")
                .InSchema("resources")
                .AsInt32().NotNullable()
                .SetExistingRowsTo(0);

            UpdateIronNumbers();
            UpdateLimestoneNumbers();
            UpdateCopperNumbers();
            UpdateCoalNumbers();
            UpdateCateriumNumbers();
            UpdateBauxiteNumbers();
            UpdateSulfurNumbers();
            UpdateRawQuartzNumbers();
            UpdateUraniumNumbers();
            UpdateOilNumbers();
        }

        private void UpdateOilNumbers()
        {
            // Blue Crater
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode154",
                "Persistent_Level:PersistentLevel.BP_ResourceNode155",
                "Persistent_Level:PersistentLevel.BP_ResourceNode98",
                "Persistent_Level:PersistentLevel.BP_ResourceNode100",
                "Persistent_Level:PersistentLevel.BP_ResourceNode151",
                "Persistent_Level:PersistentLevel.BP_ResourceNode152_995");

            // Desert Canyons
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode446_1",
                "Persistent_Level:PersistentLevel.BP_ResourceNode448",
                "Persistent_Level:PersistentLevel.BP_ResourceNode447",
                "Persistent_Level:PersistentLevel.BP_ResourceNode444_0");

            // Lake Forest
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode460", "Lake Forest");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode459",
                "Persistent_Level:PersistentLevel.BP_ResourceNode458",
                "Persistent_Level:PersistentLevel.BP_ResourceNode460");

            // Spire Coast
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode16", "Spire Coast");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode15", "Spire Coast");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode14_609", "Spire Coast");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode28_101",
                "Persistent_Level:PersistentLevel.BP_ResourceNode16",
                "Persistent_Level:PersistentLevel.BP_ResourceNode15",
                "Persistent_Level:PersistentLevel.BP_ResourceNode27_100",
                "Persistent_Level:PersistentLevel.BP_ResourceNode26_99",
                "Persistent_Level:PersistentLevel.BP_ResourceNode24_97",
                "Persistent_Level:PersistentLevel.BP_ResourceNode14_609",
                "Persistent_Level:PersistentLevel.BP_ResourceNode25_98",
                "Persistent_Level:PersistentLevel.BP_ResourceNode29_102",
                "Persistent_Level:PersistentLevel.BP_ResourceNode23_96",
                "Persistent_Level:PersistentLevel.BP_ResourceNode32_105",
                "Persistent_Level:PersistentLevel.BP_ResourceNode31_104",
                "Persistent_Level:PersistentLevel.BP_ResourceNode30_103");

            // Western Beaches
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode88", "Western Beaches");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode89", "Western Beaches");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode86", "Western Beaches");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode87", "Western Beaches");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode89",
                "Persistent_Level:PersistentLevel.BP_ResourceNode88",
                "Persistent_Level:PersistentLevel.BP_ResourceNode87",
                "Persistent_Level:PersistentLevel.BP_ResourceNode86");
        }

        private void UpdateUraniumNumbers()
        {
            // Red Bamboo Fields
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode576");

            // Red Jungle
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode598_0");

            // Rocky Desert
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode632");

            // Swamp
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode484");
        }

        private void UpdateRawQuartzNumbers()
        {
            // Dune Desert
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode231",
                "Persistent_Level:PersistentLevel.BP_ResourceNode232");

            // Eastern Dune Forest
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode137_2248",
                "Persistent_Level:PersistentLevel.BP_ResourceNode136");

            // Red Jungle
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode522",
                "Persistent_Level:PersistentLevel.BP_ResourceNode520");

            // Rocky Desert
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode552",
                "Persistent_Level:PersistentLevel.BP_ResourceNode588");

            // Northern Forest
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode56_190", "Northern Forest");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode57_373", "Northern Forest");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode56_190",
                "Persistent_Level:PersistentLevel.BP_ResourceNode57_373");

            // Titan Forest
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode527",
                "Persistent_Level:PersistentLevel.BP_ResourceNode473",
                "Persistent_Level:PersistentLevel.BP_ResourceNode471",
                "Persistent_Level:PersistentLevel.BP_ResourceNode526",
                "Persistent_Level:PersistentLevel.BP_ResourceNode474",
                "Persistent_Level:PersistentLevel.BP_ResourceNode525");
        }

        private void UpdateSulfurNumbers()
        {
            // Crater Lakes
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode170_363", "Crater Lakes");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode170_363");

            // Dune Desert
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode613");

            // Eastern Dune Forest
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode482");

            // Grass Fields
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode177");

            // Maze Canyons
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode461", "Maze Canyons");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode461");

            // Snaketree Forest
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode71_736");

            // Northern Forest
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode455", "Northern Forest");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode510", "Northern Forest");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode455",
                "Persistent_Level:PersistentLevel.BP_ResourceNode510");

            // Spire Coast
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode624");

            // Swamp
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode582");

            // Titan Forest
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode467");
        }

        private void UpdateCateriumNumbers()
        {
            // Crater Lakes
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode169_80", "Crater Lakes");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode169_80");

            // Desert Canyons
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode475", "Desert Canyons");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode475");

            // Dune Desert
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode142",
                "Persistent_Level:PersistentLevel.BP_ResourceNode240");

            // Eastern Dune Forest
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode134_8590", "Eastern Dune Forest");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode574",
                "Persistent_Level:PersistentLevel.BP_ResourceNode134_8590");

            // Grass Fields
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode70_3132",
                "Persistent_Level:PersistentLevel.BP_ResourceNode72_998");

            // Jungle Spires
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode176", "Jungle Spires");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode176");

            // Maze Canyons
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode468", "Maze Canyons");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode468");

            // Northern Forest
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode58_504",
                "Persistent_Level:PersistentLevel.BP_ResourceNode487");

            // Rocky Desert
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode121_4877");

            // Spire Coast
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode140");

            // Swamp
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode483", "Swamp");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode570", "Swamp");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode483",
                "Persistent_Level:PersistentLevel.BP_ResourceNode570");
        }

        private void UpdateCoalNumbers()
        {
            // Blue Crater
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode500",
                "Persistent_Level:PersistentLevel.BP_ResourceNode502",
                "Persistent_Level:PersistentLevel.BP_ResourceNode499",
                "Persistent_Level:PersistentLevel.BP_ResourceNode501");

            // Crater Lakes
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode130", "Crater Lakes");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode129", "Crater Lakes");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode_700", "Crater Lakes");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode130",
                "Persistent_Level:PersistentLevel.BP_ResourceNode129",
                "Persistent_Level:PersistentLevel.BP_ResourceNode_700");

            // Dune Desert
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode615",
                "Persistent_Level:PersistentLevel.BP_ResourceNode581",
                "Persistent_Level:PersistentLevel.BP_ResourceNode623",
                "Persistent_Level:PersistentLevel.BP_ResourceNode625",
                "Persistent_Level:PersistentLevel.BP_ResourceNode498",
                "Persistent_Level:PersistentLevel.BP_ResourceNode616",
                "Persistent_Level:PersistentLevel.BP_ResourceNode620",
                "Persistent_Level:PersistentLevel.BP_ResourceNode617",
                "Persistent_Level:PersistentLevel.BP_ResourceNode618",
                "Persistent_Level:PersistentLevel.BP_ResourceNode619",
                "Persistent_Level:PersistentLevel.BP_ResourceNode610",
                "Persistent_Level:PersistentLevel.BP_ResourceNode594",
                "Persistent_Level:PersistentLevel.BP_ResourceNode612",
                "Persistent_Level:PersistentLevel.BP_ResourceNode611");

            // Eastern Dune Desert
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode572",
                "Persistent_Level:PersistentLevel.BP_ResourceNode573");

            // Grass Fields
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode587",
                "Persistent_Level:PersistentLevel.BP_ResourceNode590");

            // Maze Canyons
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode452", "Maze Canyons");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode451", "Maze Canyons");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode469", "Maze Canyons");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode449", "Maze Canyons");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode452",
                "Persistent_Level:PersistentLevel.BP_ResourceNode451",
                "Persistent_Level:PersistentLevel.BP_ResourceNode469",
                "Persistent_Level:PersistentLevel.BP_ResourceNode449");

            // Northern Forest
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode456", "Northern Forest");
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode456");

            // Red Bamboo Fields
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode601", "Red Bamboo Fields");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode602", "Red Bamboo Fields");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode603", "Red Bamboo Fields");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode601",
                "Persistent_Level:PersistentLevel.BP_ResourceNode602",
                "Persistent_Level:PersistentLevel.BP_ResourceNode603",
                "Persistent_Level:PersistentLevel.BP_ResourceNode604");

            // Red Jungle
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode600", "Red Jungle");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode606",
                "Persistent_Level:PersistentLevel.BP_ResourceNode605",
                "Persistent_Level:PersistentLevel.BP_ResourceNode609",
                "Persistent_Level:PersistentLevel.BP_ResourceNode600",
                "Persistent_Level:PersistentLevel.BP_ResourceNode599");

            // Rocky Desert
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode122",
                "Persistent_Level:PersistentLevel.BP_ResourceNode7_380",
                "Persistent_Level:PersistentLevel.BP_ResourceNode5_381",
                "Persistent_Level:PersistentLevel.BP_ResourceNode6_379");

            // Spire Coast
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode621",
                "Persistent_Level:PersistentLevel.BP_ResourceNode622");

            // Western Dune Desert
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode504",
                "Persistent_Level:PersistentLevel.BP_ResourceNode503",
                "Persistent_Level:PersistentLevel.BP_ResourceNode559",
                "Persistent_Level:PersistentLevel.BP_ResourceNode560");
        }

        private void UpdateCopperNumbers()
        {
            // Blue Crater   
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode75_6425",
                "Persistent_Level:PersistentLevel.BP_ResourceNode94_406");

            // Desert Canyons
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode38_902");

            // Dune Desert
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode215",
                "Persistent_Level:PersistentLevel.BP_ResourceNode237",
                "Persistent_Level:PersistentLevel.BP_ResourceNode192_0",
                "Persistent_Level:PersistentLevel.BP_ResourceNode188",
                "Persistent_Level:PersistentLevel.BP_ResourceNode185",
                "Persistent_Level:PersistentLevel.BP_ResourceNode214",
                "Persistent_Level:PersistentLevel.BP_ResourceNode212",
                "Persistent_Level:PersistentLevel.BP_ResourceNode211",
                "Persistent_Level:PersistentLevel.BP_ResourceNode235",
                "Persistent_Level:PersistentLevel.BP_ResourceNode213",
                "Persistent_Level:PersistentLevel.BP_ResourceNode216",
                "Persistent_Level:PersistentLevel.BP_ResourceNode141",
                "Persistent_Level:PersistentLevel.BP_ResourceNode195",
                "Persistent_Level:PersistentLevel.BP_ResourceNode194",
                "Persistent_Level:PersistentLevel.BP_ResourceNode197");

            // Eastern Dune Forest
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode150", "Eastern Dune Forest");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode150");

            // Grass Fields
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode505",
                "Persistent_Level:PersistentLevel.BP_ResourceNode506",
                "Persistent_Level:PersistentLevel.BP_ResourceNode507",
                "Persistent_Level:PersistentLevel.BP_ResourceNode540",
                "Persistent_Level:PersistentLevel.BP_ResourceNode492",
                "Persistent_Level:PersistentLevel.BP_ResourceNode493",
                "Persistent_Level:PersistentLevel.BP_ResourceNode547",
                "Persistent_Level:PersistentLevel.BP_ResourceNode68_2514");

            // Jungle Spires
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode83", "Jungle Spires");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode83");

            // Northern Forest
            UpdateBiomeNumbers(
                "Persistent_Exploration:PersistentLevel.BP_ResourceNode69",
                "Persistent_Level:PersistentLevel.BP_ResourceNode53_510",
                "Persistent_Level:PersistentLevel.BP_ResourceNode445");

            // Rocky Desert
            UpdateBiomeNumbers(
                "Persistent_Level:PersistentLevel.BP_ResourceNode162_5199",
                "Persistent_Level:PersistentLevel.BP_ResourceNode131",
                "Persistent_Level:PersistentLevel.BP_ResourceNode159",
                "Persistent_Level:PersistentLevel.BP_ResourceNode125_5930",
                "Persistent_Level:PersistentLevel.BP_ResourceNode156",
                "Persistent_Level:PersistentLevel.BP_ResourceNode111_3367",
                "Persistent_Level:PersistentLevel.BP_ResourceNode112",
                "Persistent_Level:PersistentLevel.BP_ResourceNode117");

            // Snaketree Forest
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode127",
                "Persistent_Level:PersistentLevel.BP_ResourceNode593");

            // Spire Coast
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode202");

            // Swamp
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode92", "Swamp");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode92",
                "Persistent_Level:PersistentLevel.BP_ResourceNode179",
                "Persistent_Level:PersistentLevel.BP_ResourceNode91_785");

            // Titan Forest
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode515",
                "Persistent_Level:PersistentLevel.BP_ResourceNode513",
                "Persistent_Level:PersistentLevel.BP_ResourceNode514");

            // Western Dune Forest
            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode562");
        }

        private void RelocateNodeToBiome(string instanceName, string newBiome) =>
            Update.Table("nodes").InSchema("resources")
                .Set(new
                {
                    biome = newBiome
                })
                .Where(new
                {
                    instance_name = instanceName
                });

        private void UpdateBiomeNumbers(params string[] instanceNames)
        {
            var biomeNumber = 0;
            foreach (var instanceName in instanceNames)
                UpdateNumber(++biomeNumber, instanceName);
        }

        private void UpdateLimestoneNumbers()
        {
            // If you go east of the river that's the northern forest, not sure why the map says Rocky Desert
            Update.Table("nodes").InSchema("resources")
                .Set(new
                {
                    biome = "Northern Forest"
                })
                .Where(new
                {
                    instance_name = "Persistent_Level:PersistentLevel.BP_ResourceNode463"
                });

            // That's totally the swamp, it's right down in the muck
            Update.Table("nodes").InSchema("resources")
                .Set(new
                {
                    biome = "Swamp"
                })
                .Where(new
                {
                    instance_name = "Persistent_Level:PersistentLevel.BP_ResourceNode69"
                });

            // Ground is sand, it's the desert
            Update.Table("nodes").InSchema("resources")
                .Set(new
                {
                    biome = "Desert Canyons"
                })
                .Where(new
                {
                    instance_name = "Persistent_Level:PersistentLevel.BP_ResourceNode138_590"
                });

            Update.Table("nodes").InSchema("resources")
                .Set(new
                {
                    biome = "Lake Forest"
                })
                .Where(new
                {
                    instance_name = "Persistent_Level:PersistentLevel.BP_ResourceNode42_1294"
                });

            // That void crater is part of grass fields
            Update.Table("nodes").InSchema("resources")
                .Set(new
                {
                    biome = "Grass Fields"
                })
                .Where(new
                {
                    instance_name = "Persistent_Level:PersistentLevel.BP_ResourceNode589"
                });

            Update.Table("nodes").InSchema("resources")
                .Set(new
                {
                    biome = "Swamp"
                })
                .Where(new
                {
                    instance_name = "Persistent_Level:PersistentLevel.BP_ResourceNode178"
                });

            // Blue Crater
            var blueCraterNumber = 0;
            UpdateNumber(++blueCraterNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode133_6963");
            UpdateNumber(++blueCraterNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode96_886");

            // Desert Canyons
            var desertCanyonsNumber = 0;
            UpdateNumber(++desertCanyonsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode466");
            UpdateNumber(++desertCanyonsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode37_178");
            UpdateNumber(++desertCanyonsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode139_909");
            UpdateNumber(++desertCanyonsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode138_590");

            // Dune Desert
            var duneDesertNumber = 0;
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode186");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode229");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode228");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode226");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode224");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode225");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode182");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode181");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode227");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode234");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode189");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode191");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode190");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode46_2284");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode193");

            // Grass Fields
            var grassFieldsNumber = 0;
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode589");

            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode575");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode557");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode555");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode586");

            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode556");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode509");

            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode511");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode534");

            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode553");

            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode550");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode548");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode549");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode584");

            // Jungle Spires
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode85", "Jungle Spires");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode84", "Jungle Spires");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode85",
                "Persistent_Level:PersistentLevel.BP_ResourceNode84");

            // Lake Forest
            UpdateNumber(1, "Persistent_Level:PersistentLevel.BP_ResourceNode42_1294");

            // Maze Canyons
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode465", "Maze Canyons");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode41_1099", "Maze Canyons");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode508", "Maze Canyons");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode465",
                "Persistent_Level:PersistentLevel.BP_ResourceNode41_1099",
                "Persistent_Level:PersistentLevel.BP_ResourceNode508");

            // Northern Forest
            var northernForestNumber = 0;
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode54_833");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode463");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode443");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode59_755");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode60_984");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode440");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode441");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode442");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode439_1");

            // Rocky Desert
            var rockyDesertNumber = 0;
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode11");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode20_3137");

            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode165");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode164");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode62");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode166");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode110");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode113");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode124_5785");

            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode157");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode163");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode144_1644");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode132_5908");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode143_1543");

            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode102_2068");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode103");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode104");

            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode145_1749");

            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode119");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode118_4340");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode158");

            // Snaketree Forest
            UpdateNumber(1, "Persistent_Level:PersistentLevel.BP_ResourceNode571");
            UpdateNumber(2, "Persistent_Level:PersistentLevel.BP_ResourceNode554");

            // Spire Coast
            var spireCoastNumber = 0;
            UpdateNumber(++spireCoastNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode187_0");
            UpdateNumber(++spireCoastNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode97_1");
            UpdateNumber(++spireCoastNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode77");
            UpdateNumber(++spireCoastNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode464");

            // Swamp
            var swampNumber = 0;
            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode69");
            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode93_5");
            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode67_2193");
            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode178");
            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode564");

            // Titan Forest
            UpdateNumber(1, "Persistent_Level:PersistentLevel.BP_ResourceNode516");

            // Western Dune Forest
            UpdateNumber(1, "Persistent_Level:PersistentLevel.BP_ResourceNode521");
            UpdateNumber(2, "Persistent_Level:PersistentLevel.BP_ResourceNode561");
            UpdateNumber(3, "Persistent_Level:PersistentLevel.BP_ResourceNode512");
        }

        private void UpdateIronNumbers()
        {
            // Mistakenly added some iron under blue canyon, definitely the crater
            Update.Table("nodes").InSchema("resources")
                .Set(new
                {
                    biome = "Blue Crater"
                })
                .Where(new
                {
                    biome = "Blue Canyon"
                });

            // Blue Crater
            var blueCraterNumber = 0;
            UpdateNumber(++blueCraterNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode74");
            UpdateNumber(++blueCraterNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode73_6071");
            UpdateNumber(++blueCraterNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode76");
            UpdateNumber(++blueCraterNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode95_579");

            // Jungle Spires
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode81", "Jungle Spires");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode80", "Jungle Spires");
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode82", "Jungle Spires");

            UpdateBiomeNumbers("Persistent_Level:PersistentLevel.BP_ResourceNode81",
                "Persistent_Level:PersistentLevel.BP_ResourceNode80",
                "Persistent_Level:PersistentLevel.BP_ResourceNode82");

            // Desert Canyons
            UpdateNumber(1, "Persistent_Level:PersistentLevel.BP_ResourceNode40");
            UpdateNumber(2, "Persistent_Level:PersistentLevel.BP_ResourceNode39");

            // Dune Desert
            var duneDesertNumber = 0;
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode230");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode233");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode236");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode35");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode217");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode223");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode218");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode221");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode222");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode49");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode219");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode238");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode206");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode208");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode220");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode173");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode13");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode12_91");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode207");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode239");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode203");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode209");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode199");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode198");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode180");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode200");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode205");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode184");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode204_0");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode210");

            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode196");
            UpdateNumber(++duneDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode201");

            // Grass Fields
            var grassFieldsNumber = 0;
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode538");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode539");

            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode551");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode497_1");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode496");

            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode494");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode495");

            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode531");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode491");

            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode488");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode489");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode490");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode530");

            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode533");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode532");

            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode592");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode591");

            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode546");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode545");

            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode585");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode558");

            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode543");
            UpdateNumber(++grassFieldsNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode544");

            // Maze Canyons
            RelocateNodeToBiome("Persistent_Level:PersistentLevel.BP_ResourceNode457", "Maze Canyons");

            UpdateNumber(1, "Persistent_Level:PersistentLevel.BP_ResourceNode457");

            // Northern Forest
            var northernForestNumber = 0;
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode55_1215");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode453");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode454");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode462");

            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode430");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode431");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode427");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode426");

            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode437_30");
            UpdateNumber(++northernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode435_26");

            // Rocky Desert
            var rockyDesertNumber = 0;
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode161");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode160");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode174");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode175");

            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode128_5242");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode167");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode168");

            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode123_5084");

            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode108");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode109");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode153");

            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode105_2463");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode106");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode107");

            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode116");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode115");
            UpdateNumber(++rockyDesertNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode114");

            // Snaketree Forest
            var snaketreeForestNumber = 0;
            UpdateNumber(++snaketreeForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode126_6409");

            UpdateNumber(++snaketreeForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode580");
            UpdateNumber(++snaketreeForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode579");
            UpdateNumber(++snaketreeForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode578");
            UpdateNumber(++snaketreeForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode577");

            // Southern Forest
            var southernForestNumber = 0;
            UpdateNumber(++southernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode146");
            UpdateNumber(++southernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode147");
            UpdateNumber(++southernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode148");
            UpdateNumber(++southernForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode149");

            // Spire Coast
            UpdateNumber(1, "Persistent_Level:PersistentLevel.BP_ResourceNode36");

            // Swamp
            var swampNumber = 0;
            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode567");

            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode65_1865");

            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode66");
            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode90_482");

            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode565_8");
            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode563");

            // Titan Forest
            var titanForestNumber = 0;
            UpdateNumber(++titanForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode528");

            UpdateNumber(++titanForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode517");
            UpdateNumber(++titanForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode518");

            UpdateNumber(++titanForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode523");
            UpdateNumber(++titanForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode524");

            UpdateNumber(++titanForestNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode583_1");

            // Western Dune Forest
            var westernDuneForest = 0;
            UpdateNumber(++westernDuneForest, "Persistent_Level:PersistentLevel.BP_ResourceNode535");
            UpdateNumber(++westernDuneForest, "Persistent_Level:PersistentLevel.BP_ResourceNode537");

            UpdateNumber(++westernDuneForest, "Persistent_Level:PersistentLevel.BP_ResourceNode542");
            UpdateNumber(++westernDuneForest, "Persistent_Level:PersistentLevel.BP_ResourceNode541");
            UpdateNumber(++westernDuneForest, "Persistent_Level:PersistentLevel.BP_ResourceNode569");
            UpdateNumber(++westernDuneForest, "Persistent_Level:PersistentLevel.BP_ResourceNode536");
        }

        private void UpdateBauxiteNumbers()
        {
            // Overruling the map, that is the swamp... you can walk out into the swamp from there and water is swamp colored.
            Update.Table("nodes").InSchema("resources")
                .Set(new
                {
                    biome = "Swamp"
                })
                .Where(new
                {
                    instance_name = "Persistent_Level:PersistentLevel.BP_ResourceNode480"
                });

            Update.Table("nodes").InSchema("resources")
                .Set(new
                {
                    biome = "Swamp"
                })
                .Where(new
                {
                    instance_name = "Persistent_Level:PersistentLevel.BP_ResourceNode481"
                });

            // Bauxite - Red Bamboo Fields
            UpdateNumber(1, "Persistent_Level:PersistentLevel.BP_ResourceNode486");
            UpdateNumber(2, "Persistent_Level:PersistentLevel.BP_ResourceNode566");
            UpdateNumber(3, "Persistent_Level:PersistentLevel.BP_ResourceNode529");
            UpdateNumber(4, "Persistent_Level:PersistentLevel.BP_ResourceNode568");

            // Bauxite - Red Jungle
            UpdateNumber(1, "Persistent_Level:PersistentLevel.BP_ResourceNode595");
            UpdateNumber(2, "Persistent_Level:PersistentLevel.BP_ResourceNode597");
            UpdateNumber(3, "Persistent_Level:PersistentLevel.BP_ResourceNode485");
            UpdateNumber(4, "Persistent_Level:PersistentLevel.BP_ResourceNode596");

            // Bauxite - Swamp
            var swampNumber = 0;
            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode477");
            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode476");
            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode479");
            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode480");
            UpdateNumber(++swampNumber, "Persistent_Level:PersistentLevel.BP_ResourceNode481");

            // Bauxite - Titan Forest
            UpdateNumber(1, "Persistent_Level:PersistentLevel.BP_ResourceNode633");
            UpdateNumber(2, "Persistent_Level:PersistentLevel.BP_ResourceNode634");
            UpdateNumber(3, "Persistent_Level:PersistentLevel.BP_ResourceNode636");
            UpdateNumber(4, "Persistent_Level:PersistentLevel.BP_ResourceNode635");
        }

        private void UpdateNumber(int number, string instanceName)
        {
            Update.Table("nodes").InSchema("resources")
                .Set(new
                {
                    number
                })
                .Where(new
                {
                    instance_name = instanceName
                });
        }

        public override void Down()
            => Delete
                .Column("number")
                .FromTable("nodes")
                .InSchema("resources");
    }
}