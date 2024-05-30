using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.BuildingBlocks.Infrastructure.Configuration.Processing
{
    public class LoggingNotificationHandlerDecorator<T>(INotificationHandler<T> decorated, ILogger logger) : INotificationHandler<T>
        where T : INotification
    {
        public async Task Handle(T notification, CancellationToken cancellationToken)
        {
            using (LogContext.Push(new NotificationLogEnricher(notification)))
            {
                var notificationName = notification.GetType().Name;
                try
                {
                    if (notification is IDomainEventNotification)
                        logger.Information("Sending {Notification}", notificationName);
                    else
                        logger.Information("Publishing {Event}", notificationName);

                    await decorated.Handle(notification, cancellationToken);

                    if (notification is IDomainEventNotification)
                        logger.Information("Successfully sent {Notification}", notificationName);
                    else
                        logger.Information("Published {Event}", notificationName);
                }
                catch (Exception exception)
                {
                    if (notification is IDomainEventNotification)
                        logger.Error(exception, "Sending {Notification} failed", notificationName);
                    else
                        logger.Error(exception, "Publishing {Event} failed", notificationName);

                    throw;
                }
            }
        }

        private class NotificationLogEnricher(INotification notification) : ILogEventEnricher
        {
            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                if (notification is IDomainEventNotification domainEventNotification)
                {
                    logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"Notification:{domainEventNotification.Id}")));
                }
                else if (notification is IntegrationEvent integrationEvent)
                {
                    logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"IntegrationEvent:{integrationEvent.Id}")));
                }
                else if (notification is DomainEventBase domainEventBase)
                {
                    logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"DomainEvent:{domainEventBase.Id}")));
                }
            }
        }
    }
}
