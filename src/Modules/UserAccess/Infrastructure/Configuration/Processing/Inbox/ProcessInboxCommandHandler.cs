using Dapper;
using MediatR;
using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.UserAccess.Application.Configuration.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Processing.Inbox
{
    internal class ProcessInboxCommandHandler : ICommandHandler<ProcessInboxCommand>
    {
        private readonly IMediator _mediator;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ProcessInboxCommandHandler(IMediator mediator, IDbConnectionFactory dbConnectionFactory)
        {
            _mediator = mediator;
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<Unit> Handle(ProcessInboxCommand command, CancellationToken cancellationToken)
        {
            //TODO fix
            var connection = _dbConnectionFactory.GetOpenConnection();
            var sql = "SELECT " +
                         $"[InboxMessage].[Id] AS [{nameof(InboxMessageDto.Id)}], " +
                         $"[InboxMessage].[Type] AS [{nameof(InboxMessageDto.Type)}], " +
                         $"[InboxMessage].[Data] AS [{nameof(InboxMessageDto.Data)}] " +
                         "FROM [users].[InboxMessages] AS [InboxMessage] " +
                         "WHERE [InboxMessage].[ProcessedDate] IS NULL " +
                         "ORDER BY [InboxMessage].[OccurredOn]";

            var messages = await connection.QueryAsync<InboxMessageDto>(sql);

            const string sqlUpdateProcessedDate = "UPDATE [users].[InboxMessages] " +
                                                  "SET [ProcessedDate] = @Date " +
                                                  "WHERE [Id] = @Id";

            foreach (var message in messages)
            {
                var messageAssembly = AppDomain.CurrentDomain.GetAssemblies()
                    .SingleOrDefault(assembly => message.Type.Contains(assembly.GetName().Name));

                var type = messageAssembly.GetType(message.Type);
                var request = JsonConvert.DeserializeObject(message.Data, type);

                try
                {
                    await _mediator.Publish((INotification)request, cancellationToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

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