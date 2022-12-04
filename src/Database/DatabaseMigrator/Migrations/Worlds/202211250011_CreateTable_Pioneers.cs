using FluentMigrator;

namespace DatabaseMigrator.Migrations.Worlds
{
    [Migration(202211250011)]
    public class CreateTable_Pioneers : Migration
    {
        public override void Down()
        {
            Delete
                .Table("pioneers")
                .InSchema("pioneers");
        }

        public override void Up()
        {
            Create.Table("pioneers")
                .InSchema("pioneers")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("auth0_user_id").AsString().NotNullable().Unique();
        }
    }
}
