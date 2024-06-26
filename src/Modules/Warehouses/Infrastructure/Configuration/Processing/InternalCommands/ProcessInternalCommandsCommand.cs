using Dapper;
using MediatR;
using Newtonsoft.Json;
using Polly;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Warehouses.Application.Configuration;
using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;
using System.Data;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.Processing.InternalCommands
{
    internal record ProcessInternalCommandsCommand : CommandBase, IRecurringCommand { }

    internal class ProcessInternalCommandsCommandHandler(
        IDbConnectionFactory dbConnectionFactory) : ICommandHandler<ProcessInternalCommandsCommand>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

        public async Task<Unit> Handle(ProcessInternalCommandsCommand command, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            var sql =
                $" SELECT id AS {nameof(InternalCommandDto.Id)}, " +
                $"        type AS {nameof(InternalCommandDto.Type)}, " +
                $"        data AS {nameof(InternalCommandDto.Data)} " +
                "    FROM warehouses.internal_commands " +
                "   WHERE processed_date IS NULL " +
                "ORDER BY enqueue_date";
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
            const string errorSql = "UPDATE warehouses.internal_commands " +
                                    "   SET processed_date = @NowDate, " +
                                    "       error          = @Error " +
                                    " WHERE id = @Id";
            await connection.ExecuteScalarAsync(
                errorSql,
                new
                {
                    NowDate = DateTime.UtcNow,
                    Error = result.FinalException.ToString(),
                    Id = id
                });
        }

        private async Task ProcessCommand(InternalCommandDto internalCommand)
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