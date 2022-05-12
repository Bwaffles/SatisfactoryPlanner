using FluentMigrator;

namespace DatabaseMigrator.Migrations.App
{
    [Migration(202205121240)]
    public class CreateSchema_App : Migration
    {
        public override void Down()
        {
            Delete
                .Schema("app");
        }

        public override void Up()
        {
            Create
                .Schema("app");
        }
    }
}
