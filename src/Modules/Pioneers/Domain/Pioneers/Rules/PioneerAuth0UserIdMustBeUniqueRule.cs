using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers.Rules
{
    /// <summary>
    ///     Check that the auth0 user id isn't already assigned to a pioneer.
    ///     This is how we connect the auth0 login to the pioneer in the database so they
    ///     can do work.
    /// </summary>
    public class PioneerAuth0UserIdMustBeUniqueRule : IBusinessRule
    {
        private readonly string _auth0UserId;
        private readonly IPioneersCounter _pioneersCounter;

        public PioneerAuth0UserIdMustBeUniqueRule(string auth0UserId, IPioneersCounter pioneersCounter)
        {
            _auth0UserId = auth0UserId;
            _pioneersCounter = pioneersCounter;
        }

        public string Message => "Pioneer's Auth0UserId must be unique.";

        public bool IsBroken() => _pioneersCounter.CountPioneersWithAuth0UserId(_auth0UserId) > 0;
    }
}