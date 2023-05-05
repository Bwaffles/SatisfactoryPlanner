using FluentValidation;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DecreaseExtractionRate
{
    // ReSharper disable once UnusedMember.Global
    internal class DecreaseExtractionRateCommandValidator : AbstractValidator<DecreaseExtractionRateCommand>
    {
        public DecreaseExtractionRateCommandValidator()
        {
            RuleFor(_ => _.WorldId).NotEmpty()
                .WithMessage("Id of world cannot be empty.");

            RuleFor(_ => _.NodeId).NotEmpty()
                .WithMessage("Id of node cannot be empty.");

            RuleFor(_ => _.ExtractionRate).GreaterThanOrEqualTo(0)
                .WithMessage("The extraction rate must be greater than or equal to 0.");
        }
    }
}