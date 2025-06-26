using FluentMigrator;

namespace DatabaseMigrator.Migrations.Resources.Resources
{
    /// <summary>
    ///     Adding SAM since it was implemented fully in update 1.0.
    /// </summary>
    [Migration(202506251112)]
    public class Insert_SAM : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("resources").InSchema("resources")
                .Row(new
                {
                    code = "Desc_SAM_C",
                    name = "SAM",
                    description = "Strange Alien Matter, commonly referred to as SAM, doesn't seem to follow the known laws of physics. It whispers of new possibilities.",
                    stack_size = 100,
                    resource_sink_points = 20,
                    energy_value = (double?)null,
                    radioactive_decay = (double?)null,
                    resource_form = "RF_SOLID"
                });
        }
    }
}