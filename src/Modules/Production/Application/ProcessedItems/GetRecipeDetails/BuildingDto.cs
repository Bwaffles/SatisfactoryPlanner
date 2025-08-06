namespace SatisfactoryPlanner.Modules.Production.Application.ProcessedItems.GetRecipeDetails
{
    public class BuildingDto
    {
        /// <summary>
        /// The id of the building.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the building.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The method of production that can be used in this building, either Automatic or Manual.
        /// </summary>
        public string ProductionMethod { get; set; }
    }
}
