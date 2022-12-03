using FluentMigrator;

namespace DatabaseMigrator.Migrations.Pioneers
{
    [Migration(202211250010)]
    public class CreateSchema_Pioneers : Migration
    {
        public override void Down()
        {
            Delete
                .Schema("pioneers");
        }

        public override void Up()
        {
            Create
                .Schema("pioneers");
        }
    }
}
