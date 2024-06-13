using Dapper;
using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Serialization;
using SatisfactoryPlanner.Modules.Warehouses.Application.Configuration;
using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.Processing.InternalCommands
{
    public class CommandsScheduler(IDbConnectionFactory dbConnectionFactory) : ICommandsScheduler
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task EnqueueAsync(ICommand command)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();
            const string sqlInsert = "INSERT INTO warehouses.internal_commands (id, enqueue_date, type, data) VALUES " +
                                     "(@Id, @EnqueueDate, @Type, @Data)";

            await connection.ExecuteAsync(sqlInsert, new
            {
                command.Id,
                EnqueueDate = DateTime.UtcNow,
                Type = command.GetType().FullName,
                Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                })
            });
        }
    }
}