using FluentMigrator;

namespace DatabaseMigrator.Migrations.Warehouses
{
    [Migration(202406131012)]
    public class CreateSchema_Warehouses : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Schema("warehouses");
        }
    }
}
