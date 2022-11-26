namespace SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.GetPioneerDetails
{
    public class PioneerDetailsDto
    {
        /// <summary>
        ///     The unique id of the pioneer.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The identifier of the pioneer in Auth0.
        /// </summary>
        public string Auth0UserId { get; set; }
    }
}