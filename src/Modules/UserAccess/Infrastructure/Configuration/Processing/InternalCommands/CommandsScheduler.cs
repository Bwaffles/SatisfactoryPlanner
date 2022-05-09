using Dapper;
using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Serialization;
using SatisfactoryPlanner.UserAccess.Application.Configuration.Commands;
using SatisfactoryPlanner.UserAccess.Application.Contracts;
using System;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.UserAccess.Infrastructure.Configuration.Processing.InternalCommands
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
            var connection = this._dbConnectionFactory.GetOpenConnection();
            // TODO fix
            const string sqlInsert = "INSERT INTO [users].[InternalCommands] ([Id], [EnqueueDate] , [Type], [Data]) VALUES " +
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
            var connection = this._dbConnectionFactory.GetOpenConnection();
            // TODO fix
            const string sqlInsert = "INSERT INTO [users].[InternalCommands] ([Id], [EnqueueDate] , [Type], [Data]) VALUES " +
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