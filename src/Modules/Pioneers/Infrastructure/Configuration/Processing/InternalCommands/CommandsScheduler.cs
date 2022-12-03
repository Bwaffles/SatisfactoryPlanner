using Dapper;
using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Serialization;
using SatisfactoryPlanner.Modules.Pioneers.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Pioneers.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration.Processing.InternalCommands
{
    public class CommandsScheduler : ICommandsScheduler
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CommandsScheduler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task EnqueueAsync(ICommand command)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();
            const string sqlInsert = "INSERT INTO pioneers.internal_commands (id, enqueue_date, type, data) VALUES " +
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

        public async Task EnqueueAsync<T>(ICommand<T> command)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();
            const string sqlInsert = "INSERT INTO pioneers.internal_commands (id, enqueue_date, type, data) VALUES " +
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