using FluentMigrator;

namespace DatabaseMigrator.Migrations.Factories
{
    [Migration(202204021156)]
    public class AddColumn_BuiltUnderFactoryId_ToFactories : Migration
    {
        public override void Down()
        {
            Delete.Column("built_under_factory_id").FromTable("factories").InSchema("factories");
        }

        public override void Up()
        {
            Create.Column("built_under_factory_id").OnTable("factories").InSchema("factories")
                .AsGuid().Nullable().ForeignKey("", "factories", "factories", "id");
        }
    }
}
