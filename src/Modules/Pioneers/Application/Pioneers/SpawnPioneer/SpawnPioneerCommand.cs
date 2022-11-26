using SatisfactoryPlanner.Modules.Pioneers.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.SpawnPioneer
{
    public class SpawnPioneerCommand : CommandBase<Guid>
    {
        public string Auth0UserId { get; }

        public SpawnPioneerCommand(string auth0UserId) => Auth0UserId = auth0UserId;
    }
}