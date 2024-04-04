using FluentMigrator;

namespace DatabaseMigrator.Migrations.Production
{
    [Migration(202404031542)]
    public class AlterSchema_FactoriesToProduction : Migration
    {
        public override void Down()
        {
            Execute.Sql("ALTER SCHEMA production RENAME TO factories");
        }

        public override void Up()
        {
            Execute.Sql("ALTER SCHEMA factories RENAME TO production");
        }
    }
}
