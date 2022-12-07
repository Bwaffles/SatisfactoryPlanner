using FluentMigrator;

namespace DatabaseMigrator.Migrations.Worlds
{
    [Migration(202212031936)]
    public class CreateTable_Worlds : Migration
    {
        public override void Down()
        {
            Delete
                .Table("worlds")
                .InSchema("pioneers");
        }

        public override void Up()
        {
            Create.Table("worlds")
                .InSchema("pioneers")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("creator_id").AsGuid().NotNullable()
                .WithColumn("create_date").AsDateTime().NotNullable();
        }
    }
}
