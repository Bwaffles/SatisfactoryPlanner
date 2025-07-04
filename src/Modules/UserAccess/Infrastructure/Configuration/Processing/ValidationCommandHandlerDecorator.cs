﻿using FluentValidation;
using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.UserAccess.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    internal class ValidationCommandHandlerDecorator<T> : ICommandHandler<T>
        where T : ICommand
    {
        private readonly ICommandHandler<T> _decorated;
        private readonly IList<IValidator<T>> _validators;

        public ValidationCommandHandlerDecorator(
            IList<IValidator<T>> validators,
            ICommandHandler<T> decorated)
        {
            _validators = validators;
            _decorated = decorated;
        }

        public Task<Unit> Handle(T command, CancellationToken cancellationToken)
        {
            var errors = _validators
                .Select(v => v.Validate(command))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (errors.Any())
                throw new InvalidCommandException(errors.Select(x => x.ErrorMessage).ToList());

            return _decorated.Handle(command, cancellationToken);
        }
    }
}