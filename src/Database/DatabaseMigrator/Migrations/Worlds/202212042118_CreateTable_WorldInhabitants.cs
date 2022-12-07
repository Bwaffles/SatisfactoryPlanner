using FluentMigrator;

namespace DatabaseMigrator.Migrations.Worlds
{
    [Migration(202212042118)]
    public class CreateTable_WorldInhabitants : Migration
    {
        public override void Down()
        {
            Delete
                .Table("world_inhabitants")
                .InSchema("worlds");
        }

        public override void Up()
        {
            Create.Table("world_inhabitants")
                .InSchema("worlds")
                .WithColumn("world_id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("pioneer_id").AsGuid().NotNullable().PrimaryKey();
        }
    }
}
