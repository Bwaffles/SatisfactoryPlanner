using FluentMigrator;
using System.Linq;
using System.Reflection;

namespace DatabaseMigrator.Views
{
    /// <summary>
    /// A migration that will run after migrations have been applied to run all of the views.
    /// To add a new view, create a new sql file anywhere in this Views directory to create or replace the view.
    /// </summary>
    [Maintenance(MigrationStage.AfterAll)]
    public class ExecuteViews : Migration
    {
        public override void Down()
        {
            // No-op
        }

        public override void Up()
        {
            var views = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceNames()
                .Where(_ => _.StartsWith("Views/"));

            foreach (var view in views)
                Execute.EmbeddedScript(view);
        }
    }
}
