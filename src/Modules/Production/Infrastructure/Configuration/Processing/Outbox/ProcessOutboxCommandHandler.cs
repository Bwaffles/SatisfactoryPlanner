using Dapper;
using MediatR;
using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using SatisfactoryPlanner.Modules.Production.Application.Configuration.Commands;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Processing.Outbox
{
    // ReSharper disable once UnusedMember.Global
    internal class ProcessOutboxCommandHandler : ICommandHandler<ProcessOutboxCommand>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IDomainEventNotificationMapper _domainEventNotificationMapper;
        private readonly IMediator _mediator;

        public ProcessOutboxCommandHandler(
            IMediator mediator,
            IDbConnectionFactory dbConnectionFactory,
            IDomainEventNotificationMapper domainNotificationsMapper)
        {
            _mediator = mediator;
            _dbConnectionFactory = dbConnectionFactory;
            _domainEventNotificationMapper = domainNotificationsMapper;
        }

        public async Task<Unit> Handle(ProcessOutboxCommand command, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            const string getOutboxMessages = $"  SELECT outbox_message.id AS {nameof(OutboxMessageDto.Id)}, " +
                                             $"         outbox_message.type AS {nameof(OutboxMessageDto.Type)}, " +
                                             $"         outbox_message.data AS {nameof(OutboxMessageDto.Data)} " +
                                             "     FROM production.outbox_messages AS outbox_message " +
                                             "    WHERE outbox_message.processed_date IS NULL " +
                                             " ORDER BY outbox_message.occurred_on";
            var messages = await connection.QueryAsync<OutboxMessageDto>(getOutboxMessages);

            foreach (var message in messages)
            {
                var type = _domainEventNotificationMapper.GetType(message.Type);
                var @event = (JsonConvert.DeserializeObject(message.Data, type) as IDomainEventNotification)!;

                using (LogContext.Push(new OutboxMessageContextEnricher(@event)))
                {
                    await _mediator.Publish(@event, cancellationToken);

                    const string sqlUpdateProcessedDate = "UPDATE production.outbox_messages " +
                                                          "   SET processed_date = @Date " +
                                                          " WHERE id = @Id";
                    await connection.ExecuteAsync(sqlUpdateProcessedDate,
                        new
                        {
                            Date = DateTime.UtcNow, message.Id
                        });
                }
            }

            return Unit.Value;
        }

        private class OutboxMessageContextEnricher : ILogEventEnricher
        {
            private readonly IDomainEventNotification _notification;

            public OutboxMessageContextEnricher(IDomainEventNotification notification) => _notification = notification;

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) =>
                logEvent.AddOrUpdateProperty(new LogEventProperty("Context",
                    new ScalarValue($"OutboxMessage:{_notification.Id}")));
        }
    }
}