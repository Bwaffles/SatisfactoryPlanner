using Dapper;
using MediatR;
using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Warehouses.Application.Configuration;
using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.Processing.Inbox
{
    public record ProcessInboxCommand : CommandBase, IRecurringCommand { }

    internal class ProcessInboxCommandHandler(IMediator mediator, IDbConnectionFactory dbConnectionFactory) : ICommandHandler<ProcessInboxCommand>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
        private readonly IMediator _mediator = mediator;

        public async Task<Unit> Handle(ProcessInboxCommand command, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();
            const string sql =
                $" SELECT id AS {nameof(InboxMessageDto.Id)}, " +
                $"        type AS {nameof(InboxMessageDto.Type)}, " +
                $"        data AS {nameof(InboxMessageDto.Data)} " +
                "    FROM warehouses.inbox_messages " +
                "   WHERE processed_date IS NULL " +
                "ORDER BY occurred_on";

            var messages = await connection.QueryAsync<InboxMessageDto>(sql);

            foreach (var message in messages)
            {
                var messageAssembly = AppDomain.CurrentDomain.GetAssemblies()
                    .Single(assembly => message.Type.Contains(assembly.GetName().Name!));

                var type = messageAssembly.GetType(message.Type, true)!;
                var request = (JsonConvert.DeserializeObject(message.Data, type) as INotification)!;

                try
                {
                    await _mediator.Publish(request, cancellationToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                const string sqlUpdateProcessedDate = "UPDATE warehouses.inbox_messages " +
                                                      "   SET processed_date = @Date " +
                                                      " WHERE id = @Id";
                await connection.ExecuteAsync(sqlUpdateProcessedDate, new
                {
                    Date = DateTime.UtcNow,
                    message.Id
                });
            }

            return Unit.Value;
        }
    }
}