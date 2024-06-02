using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.BuildingBlocks.Domain;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public class DomainEventsDispatcherNotificationHandlerDecorator<TNotification>(
        IDomainEventsDispatcher domainEventsDispatcher,
        INotificationHandler<TNotification> decorated,
        ILogger logger)
        : INotificationHandler<TNotification> where TNotification : INotification
    {
        private readonly INotificationHandler<TNotification> _decorated = decorated;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher = domainEventsDispatcher;
        private readonly ILogger _logger = logger;

        public async Task Handle(TNotification notification, CancellationToken cancellationToken)
        {
            var handler = _decorated.GetType().Name;

            try
            {
                switch (notification)
                {
                    case IDomainEvent domainEvent:
                        _logger.Information("Processing {DomainEvent} with {Handler}", domainEvent.GetType().Name, handler);
                        break;
                    case IDomainEventNotification domainEventNotification:
                        _logger.Information("Processing {DomainEventNotification} with {Handler}", domainEventNotification.GetType().Name, handler);
                        break;
                }

                await _decorated.Handle(notification, cancellationToken);

                await _domainEventsDispatcher.DispatchEventsAsync();

                switch (notification)
                {
                    case IDomainEvent domainEvent:
                        _logger.Information("Sucessfully processed {DomainEvent} with {Handler}", domainEvent.GetType().Name, handler);
                        break;
                    case IDomainEventNotification domainEventNotification:
                        _logger.Information("Sucessfully processed {DomainEventNotification} with {Handler}", domainEventNotification.GetType().Name, handler);
                        break;
                }
            }
            catch (Exception exception)
            {
                switch (notification)
                {
                    case IDomainEvent domainEvent:
                        _logger.Error(exception, "Processing {DomainEvent} with {Handler} failed", domainEvent.GetType().Name, handler);
                        break;
                    case IDomainEventNotification domainEventNotification:
                        _logger.Error(exception, "Processing {DomainEventNotification} with {Handler} failed", domainEventNotification.GetType().Name, handler);
                        break;
                }

                throw;
            }
        }
    }
}