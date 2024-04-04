using FluentMigrator;

namespace DatabaseMigrator.Migrations.Production
{
    [Migration(202404041146)]
    public class Create_ProductionLines : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("production_lines")
                .InSchema("production")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("world_id").AsGuid().NotNullable()
                .WithColumn("name").AsString().NotNullable();
        }
    }
}
