namespace DatabaseMigrator.Scripts
{
    /// <summary>
    ///     Contains the paths to all the scripts.
    /// </summary>
    public class Scripts
    {
        /* The migrations need to be ran from the SatisfactoryPlanner main folder.
           Git Actions runs the migrations from this folder and this is the easiest way to get integration tests running.
           TODO can we customize the script folder location so we can run the console app from VS?
        */
        private static readonly string Path = "src/Database/DatabaseMigrator/Scripts";
        public static string GeneratorsPath = $"{Path}/Generators";

        public static string SeedItemsWithResources = $"{Path}/0001__seed_items_with_resources.sql";

        public static string SeedResourcesNodes = $"{Path}/0002__seed_resource_nodes.sql";

        /// <summary>
        ///     Seeds resource extractors from game files.
        /// </summary>
        public static string SeedResourceExtractors = $"{Path}/0003__seed_resource_extractors.sql";
    }
}
