using FluentValidation;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.IncreaseExtractionRate
{
    // ReSharper disable once UnusedMember.Global
    internal class IncreaseExtractionRateCommandValidator : AbstractValidator<IncreaseExtractionRateCommand>
    {
        public IncreaseExtractionRateCommandValidator()
        {
            RuleFor(_ => _.WorldId).NotEmpty()
                .WithMessage("Id of world cannot be empty.");

            RuleFor(_ => _.NodeId).NotEmpty()
                .WithMessage("Id of node cannot be empty.");

            RuleFor(_ => _.ExtractionRate).GreaterThan(0)
                .WithMessage("The extraction rate must be greater than 0.");
        }
    }
}