﻿using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Processing
{
    internal class LoggingCommandHandlerDecorator<T> : ICommandHandler<T>
        where T : ICommand
    {
        private readonly ICommandHandler<T> _decorated;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly ILogger _logger;

        public LoggingCommandHandlerDecorator(
            ILogger logger,
            IExecutionContextAccessor executionContextAccessor,
            ICommandHandler<T> decorated)
        {
            _logger = logger;
            _executionContextAccessor = executionContextAccessor;
            _decorated = decorated;
        }

        public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
        {
            // Don't want to log commands like the OutboxProcessingCommand which runs every 2 seconds
            if (command is IRecurringCommand)
                return await _decorated.Handle(command, cancellationToken);

            using (
                LogContext.Push(
                    new RequestLogEnricher(_executionContextAccessor),
                    new CommandLogEnricher(command)))
            {
                try
                {
                    _logger.Information("Executing command {Command}", command.GetType().Name);

                    var result = await _decorated.Handle(command, cancellationToken);

                    _logger.Information("Command {Command} processed successfully", command.GetType().Name);

                    return result;
                }
                catch (Exception exception)
                {
                    _logger.Error(exception, "Command {Command} processing failed", command.GetType().Name);
                    throw;
                }
            }
        }

        private class CommandLogEnricher : ILogEventEnricher
        {
            private readonly ICommand _command;

            public CommandLogEnricher(ICommand command)
            {
                _command = command;
            }

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddOrUpdateProperty(new LogEventProperty("Context",
                    new ScalarValue($"Command:{_command.Id.ToString()}")));
            }
        }

        private class RequestLogEnricher : ILogEventEnricher
        {
            private readonly IExecutionContextAccessor _executionContextAccessor;

            public RequestLogEnricher(IExecutionContextAccessor executionContextAccessor)
            {
                _executionContextAccessor = executionContextAccessor;
            }

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                if (_executionContextAccessor.IsAvailable)
                    logEvent.AddOrUpdateProperty(new LogEventProperty("CorrelationId",
                        new ScalarValue(_executionContextAccessor.CorrelationId)));
            }
        }
    }
}