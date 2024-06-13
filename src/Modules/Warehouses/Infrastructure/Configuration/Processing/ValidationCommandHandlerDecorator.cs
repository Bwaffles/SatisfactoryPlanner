using FluentValidation;
using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Warehouses.Application.Configuration;
using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.Processing
{
    internal class ValidationCommandHandlerDecorator<T>(
        IList<IValidator<T>> validators,
        ICommandHandler<T> decorated) : ICommandHandler<T>
        where T : ICommand
    {
        private readonly ICommandHandler<T> _decorated = decorated;
        private readonly IList<IValidator<T>> _validators = validators;

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