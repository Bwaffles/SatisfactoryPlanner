using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers
{
    public class PioneersCounter : IPioneersCounter
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public PioneersCounter(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

        public int CountPioneersWithAuth0UserId(string auth0UserId)
        {
            var connection = _connectionFactory.GetOpenConnection();
            const string sql = "SELECT COUNT(*) " +
                               "  FROM pioneers.pioneers AS pioneer " +
                               $"WHERE pioneer.auth0_user_id = @{nameof(auth0UserId)}";

            return connection.QuerySingle<int>(
                sql,
                new
                {
                    auth0UserId
                });
        }
    }
}