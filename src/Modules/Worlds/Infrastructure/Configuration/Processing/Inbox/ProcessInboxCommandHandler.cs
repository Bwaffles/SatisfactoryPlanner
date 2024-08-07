﻿using Dapper;
using MediatR;
using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Worlds.Application.Configuration.Commands;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.Processing.Inbox
{
    // ReSharper disable once UnusedMember.Global
    internal class ProcessInboxCommandHandler : ICommandHandler<ProcessInboxCommand>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IMediator _mediator;

        public ProcessInboxCommandHandler(IMediator mediator, IDbConnectionFactory dbConnectionFactory)
        {
            _mediator = mediator;
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<Unit> Handle(ProcessInboxCommand command, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();
            var sql =
                $" SELECT inbox_message.id AS {nameof(InboxMessageDto.Id)}, " +
                $"        inbox_message.type AS {nameof(InboxMessageDto.Type)}, " +
                $"        inbox_message.data AS {nameof(InboxMessageDto.Data)} " +
                "    FROM worlds.inbox_messages AS inbox_message " +
                "   WHERE inbox_message.processed_date IS NULL " +
                "ORDER BY inbox_message.occurred_on";

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

                const string sqlUpdateProcessedDate = "UPDATE worlds.inbox_messages " +
                                                      "   SET processed_date = @Date " +
                                                      " WHERE id = @Id";
                await connection.ExecuteAsync(sqlUpdateProcessedDate, new
                {
                    Date = DateTime.UtcNow, message.Id
                });
            }

            return Unit.Value;
        }
    }
}