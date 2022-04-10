namespace DatabaseMigrator.Scripts
{
    /// <summary>
    ///     Contains the paths to all the scripts.
    /// </summary>
    public class Scripts
    {
        private static string Path = "E:/Projects/SatisfactoryPlanner/src/Database/DatabaseMigrator/Scripts";
        public static string GeneratorsPath = $"{Path}/Generators";

        /// <summary>
        ///     Seeds resource extractors from game files.
        /// </summary>
        public static string Seed_Resource_Extractors = $"{Path}/0003__seed_resource_extractors.sql";
    }
}
