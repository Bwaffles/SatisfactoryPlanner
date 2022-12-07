using FluentMigrator;

namespace DatabaseMigrator.Migrations.Worlds
{
    [Migration(202212021250)]
    public class DeleteColumn_Auth0UserId_FromPioneers : Migration
    {
        public override void Down()
        {
            Alter
                .Table("pioneers")
                .InSchema("pioneers")
                .AddColumn("auth0_user_id").AsString().NotNullable().Unique().WithDefaultValue("<null>");
        }

        public override void Up()
        {
            Delete
                .Column("auth0_user_id")
                .FromTable("pioneers")
                .InSchema("pioneers");
        }
    }
}
