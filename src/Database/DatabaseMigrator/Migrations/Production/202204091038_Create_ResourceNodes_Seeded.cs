using DatabaseMigrator.Scripts.Generators._0002_SeedResourceNodes;
using DatabaseMigrator.Scripts.Generators.SeedResourceExtractors;
using FluentMigrator;

namespace DatabaseMigrator.Migrations.Production
{
    [Migration(202204091038)]
    public class Create_ResourceNodes_Seeded : Migration
    {
        public override void Down()
        {
            Delete
                .Table("resource_nodes")
                .InSchema("factories");
        }

        public override void Up()
        {
            Create
                .Table("resource_nodes")
                .InSchema("factories")
                .WithColumn("id")
                    .AsGuid()
                    .NotNullable()
                    .PrimaryKey()
                    .WithDefault(SystemMethods.NewGuid)
                .WithColumn("item_code")
                    .AsString()
                    .ForeignKey("", "factories", "items", "code")
                .WithColumn("purity")
                    .AsString()
                    .NotNullable()
                    .WithColumnDescription("How how fast it is to mine the resource.")
                .WithColumn("biome")
                    .AsString()
                    .NotNullable()
                    .WithColumnDescription("The biome where the node is located.")
                .WithColumn("map_position_x")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("The x co-ordinate of this node on the map.")
                .WithColumn("map_position_y")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("The y co-ordinate of this node on the map.")
                .WithColumn("map_position_z")
                    .AsDecimal()
                    .NotNullable()
                    .WithColumnDescription("The z co-ordinate of this node on the map.")
                .WithColumn("instance_name")
                    .AsString()
                    .NotNullable()
                    .WithColumnDescription("The instance of this node from the save file. Used to cross check with the save file when the game files change.")
            ;

            Execute.Sql("ALTER TABLE factories.resource_nodes " +
                        "ADD CONSTRAINT purity_check CHECK (purity in ('Impure', 'Normal', 'Pure'));");

            new SeedResourceNodesScriptGenerator()
                .Generate();

            Execute.Script(Scripts.Scripts.SeedResourcesNodes);
        }
    }
}
