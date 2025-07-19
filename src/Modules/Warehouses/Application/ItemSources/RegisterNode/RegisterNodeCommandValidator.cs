using FluentValidation;

namespace SatisfactoryPlanner.Modules.Warehouses.Application.ItemSources.RegisterNode
{
    // ReSharper disable once UnusedMember.Global
    internal class RegisterNodeCommandValidator : AbstractValidator<RegisterNodeCommand>
    {
        public RegisterNodeCommandValidator()
        {
            RuleFor(_ => _.Id).NotEmpty()
                .WithMessage("Id cannot be empty.");

            RuleFor(_ => _.WorldId).NotEmpty()
                .WithMessage("Id of world cannot be empty.");

            RuleFor(_ => _.NodeId).NotEmpty()
                .WithMessage("Id of node cannot be empty.");

            RuleFor(_ => _.ItemId).NotEmpty()
                .WithMessage("Id of item cannot be empty.");
        }
    }
}