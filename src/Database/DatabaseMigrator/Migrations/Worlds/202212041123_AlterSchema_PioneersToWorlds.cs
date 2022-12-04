using FluentMigrator;

namespace DatabaseMigrator.Migrations.Worlds
{
    [Migration(202212041123)]
    public class AlterSchema_PioneersToWorlds : Migration
    {
        public override void Down()
        {
            Create.Schema("pioneers");

            Alter
                .Table("inbox_messages")
                .InSchema("worlds")
                .ToSchema("pioneers");

            Alter
                .Table("internal_commands")
                .InSchema("worlds")
                .ToSchema("pioneers");

            Alter
                .Table("outbox_messages")
                .InSchema("worlds")
                .ToSchema("pioneers");

            Alter
                .Table("pioneers")
                .InSchema("worlds")
                .ToSchema("pioneers");

            Alter
                .Table("worlds")
                .InSchema("worlds")
                .ToSchema("pioneers");

            Delete.Schema("worlds");
        }

        public override void Up()
        {
            Create.Schema("worlds");

            Alter
                .Table("inbox_messages")
                .InSchema("pioneers")
                .ToSchema("worlds");

            Alter
                .Table("internal_commands")
                .InSchema("pioneers")
                .ToSchema("worlds");

            Alter
                .Table("outbox_messages")
                .InSchema("pioneers")
                .ToSchema("worlds");

            Alter
                .Table("pioneers")
                .InSchema("pioneers")
                .ToSchema("worlds");

            Alter
                .Table("worlds")
                .InSchema("pioneers")
                .ToSchema("worlds");

            Delete.Schema("pioneers");
        }
    }
}
