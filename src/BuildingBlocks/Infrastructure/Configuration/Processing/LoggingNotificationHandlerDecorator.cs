using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using Serilog;
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
}
