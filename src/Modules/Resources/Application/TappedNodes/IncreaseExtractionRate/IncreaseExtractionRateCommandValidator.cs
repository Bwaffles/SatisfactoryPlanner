using FluentValidation;

namespace SatisfactoryPlanner.Modules.Resources.Application.TappedNodes.IncreaseExtractionRate
{
    // ReSharper disable once UnusedMember.Global
    internal class IncreaseExtractionRateCommandValidator : AbstractValidator<IncreaseExtractionRateCommand>
    {
        public IncreaseExtractionRateCommandValidator()
        {
            RuleFor(_ => _.WorldId).NotEmpty()
                .WithMessage("Id of world cannot be empty.");

            RuleFor(_ => _.TappedNodeId).NotEmpty()
                .WithMessage("Id of tapped node cannot be empty.");

            RuleFor(_ => _.NewExtractionRate).GreaterThan(0)
                .WithMessage("The new extraction rate must be greater than 0.");
        }
    }
}