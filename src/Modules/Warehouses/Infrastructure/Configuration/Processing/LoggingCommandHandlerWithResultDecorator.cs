using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Warehouses.Application.Configuration;
using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.Processing
{
    internal class LoggingCommandHandlerWithResultDecorator<T, TResult>(
        ILogger logger,
        IExecutionContextAccessor executionContextAccessor,
        ICommandHandler<T, TResult> decorated) : ICommandHandler<T, TResult>
        where T : ICommand<TResult>
    {
        private readonly ICommandHandler<T, TResult> _decorated = decorated;
        private readonly IExecutionContextAccessor _executionContextAccessor = executionContextAccessor;
        private readonly ILogger _logger = logger;

        public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
        {
            using (
                LogContext.Push(
                    new RequestLogEnricher(_executionContextAccessor),
                    new CommandLogEnricher(command)))
            {
                try
                {
                    _logger.Information("Executing command {@Command}", command.GetType().Name);

                    var result = await _decorated.Handle(command, cancellationToken);

                    _logger.Information("Command {Command} processed successfully, result {Result}", command.GetType().Name, result);

                    return result;
                }
                catch (Exception exception)
                {
                    _logger.Error(exception, "Command processing failed");
                    throw;
                }
            }
        }

        private class CommandLogEnricher : ILogEventEnricher
        {
            private readonly ICommand<TResult> _command;

            public CommandLogEnricher(ICommand<TResult> command) => _command = command;

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) =>
                logEvent.AddOrUpdateProperty(new LogEventProperty("Context",
                    new ScalarValue($"Command:{_command.Id.ToString()}")));
        }

        private class RequestLogEnricher : ILogEventEnricher
        {
            private readonly IExecutionContextAccessor _executionContextAccessor;

            public RequestLogEnricher(IExecutionContextAccessor executionContextAccessor) =>
                _executionContextAccessor = executionContextAccessor;

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                if (_executionContextAccessor.IsAvailable)
                    logEvent.AddOrUpdateProperty(new LogEventProperty("CorrelationId",
                        new ScalarValue(_executionContextAccessor.CorrelationId)));
            }
        }
    }
}