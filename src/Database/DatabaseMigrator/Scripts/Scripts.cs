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

            Running docker compose up from command line Scripts works because it's under app/Scripts
        */
        private static readonly string Path = "Scripts";
        public static string GeneratorsPath = $"{Path}/Generators";

        public static string SeedItemsWithResources = $"{Path}/0001__seed_items_with_resources.sql";

        public static string SeedResourcesNodes = $"{Path}/0002__seed_resource_nodes.sql";

        /// <summary>
        ///     Seeds resource extractors from game files.
        /// </summary>
        public static string SeedResourceExtractors = $"{Path}/0003__seed_resource_extractors.sql";

        /// <summary>
        ///     Update the resource nodes for update 7.
        /// </summary>
        public static string UpdateResourceNodes = $"{Path}/0004__update_resource_nodes.sql";

        /// <summary>
        ///     Update the resource nodes for update 1.1.
        /// </summary>
        public static string UpdateResourceNodes1_1 = $"{Path}/0005__update_resource_nodes_v1_1.sql";
    }
}
