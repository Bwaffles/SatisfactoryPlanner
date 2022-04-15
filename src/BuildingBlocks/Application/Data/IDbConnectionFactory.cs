using System.Data;

namespace SatisfactoryPlanner.BuildingBlocks.Application.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetOpenConnection();

        IDbConnection CreateNewConnection();

        string GetConnectionString();
    }
}
