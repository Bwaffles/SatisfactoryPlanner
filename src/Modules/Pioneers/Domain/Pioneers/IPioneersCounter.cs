namespace SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers
{
    public interface IPioneersCounter
    {
        /// <summary>
        ///     Get the number of pioneers with a matching <paramref name="auth0UserId"/>.
        /// </summary>
        /// <param name="auth0UserId">The user id of the pioneer in Auth0.</param>
        int CountPioneersWithAuth0UserId(string auth0UserId);
    }
}