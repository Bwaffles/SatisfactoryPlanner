using FluentMigrator;

namespace DatabaseMigrator.Migrations.Resources
{
    [Migration(202212232105)]
    public class AddColumn_WorldId_ToTappedNodes : Migration
    {
        public override void Up() =>
            Alter
                .Table("tapped_nodes")
                .InSchema("resources")
                .AddColumn("world_id").AsGuid().NotNullable();

        public override void Down() =>
            Delete
                .Column("world_id")
                .FromTable("tapped_nodes")
                .InSchema("resources");
    }
}