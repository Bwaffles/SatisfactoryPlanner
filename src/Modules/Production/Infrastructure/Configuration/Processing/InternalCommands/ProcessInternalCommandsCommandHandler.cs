using Dapper;
using MediatR;
using Newtonsoft.Json;
using Polly;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Production.Application.Configuration.Commands;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ProcessInternalCommandsCommandHandler(IDbConnectionFactory dbConnectionFactory) =>
            _dbConnectionFactory = dbConnectionFactory;

        public async Task<Unit> Handle(ProcessInternalCommandsCommand command, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            const string sql = $" SELECT command.id AS {nameof(InternalCommandDto.Id)}, " +
                               $"        command.type AS {nameof(InternalCommandDto.Type)}, " +
                               $"        command.data AS {nameof(InternalCommandDto.Data)} " +
                               "    FROM production.internal_commands AS command " +
                               "   WHERE command.processed_date IS NULL " +
                               "ORDER BY command.enqueue_date";
            var internalCommands = await connection.QueryAsync<InternalCommandDto>(sql);

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(3)
                });

            foreach (var internalCommand in internalCommands)
            {
                var result = await policy.ExecuteAndCaptureAsync(() => ProcessCommand(internalCommand));

                if (result.Outcome == OutcomeType.Failure)
                    await UpdateCommandWithError(connection, result, internalCommand.Id);
            }

            return Unit.Value;
        }

        private static async Task UpdateCommandWithError(IDbConnection connection, PolicyResult result,
            Guid id)
        {
            const string errorSql = "UPDATE production.internal_commands " +
                                    "   SET processed_date = @NowDate, " +
                                    "       error          = @Error " +
                                    " WHERE id = @Id";
            await connection.ExecuteScalarAsync(
                errorSql,
                new
                {
                    NowDate = DateTime.UtcNow, Error = result.FinalException.ToString(), Id = id
                });
        }

        private static async Task ProcessCommand(
            InternalCommandDto internalCommand)
        {
            var type = Assemblies.Application.GetType(internalCommand.Type, true)!;
            dynamic? commandToProcess = JsonConvert.DeserializeObject(internalCommand.Data, type);

            await CommandsExecutor.Execute(commandToProcess);
        }

        private class InternalCommandDto
        {
            public Guid Id { get; set; }

            public required string Type { get; set; }

            public required string Data { get; set; }
        }
    }
}