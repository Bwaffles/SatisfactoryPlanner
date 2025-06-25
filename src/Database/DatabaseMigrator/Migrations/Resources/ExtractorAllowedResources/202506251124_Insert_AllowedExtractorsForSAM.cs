using FluentMigrator;

namespace DatabaseMigrator.Migrations.Resources.Nodes
{
    /// <summary>
    ///     Add allowed extractors for newly added SAM resource. It's a solid so it can only be extracted with miners.
    /// </summary>
    [Migration(202506251124)]
    public class Insert_AllowedExtractorsForSAM : Migration
    {
        public override void Down()
        {
            Execute.Sql("DELETE FROM resources.extractor_allowed_resources" +
                        "      WHERE resource_id = (SELECT resource.id FROM resources.resources as resource WHERE resource.code = 'Desc_SAM_C')");
        }

        public override void Up()
        {
            Execute.Sql("INSERT INTO resources.extractor_allowed_resources(extractor_id, resource_id) " +
                          "SELECT extractor.id, resource.id" +
                          "  FROM (SELECT extractor.id FROM resources.extractors as extractor WHERE extractor.code in ('Build_MinerMk1_C', 'Build_MinerMk2_C', 'Build_MinerMk3_C')) as extractor," +
                          "       (SELECT resource.id FROM resources.resources as resource WHERE resource.code = 'Desc_SAM_C') as resource");
        }
    }
}