using FluentValidation;

namespace SatisfactoryPlanner.Modules.Resources.Application.TappedNodes.TapNode
{
    // ReSharper disable once UnusedMember.Global
    internal class TapNodeCommandValidator : AbstractValidator<TapNodeCommand>
    {
        public TapNodeCommandValidator()
        {
            RuleFor(_ => _.WorldId).NotEmpty()
                .WithMessage("Id of world cannot be empty.");

            RuleFor(_ => _.NodeId).NotEmpty()
                .WithMessage("Id of node cannot be empty.");

            RuleFor(_ => _.ExtractorId).NotEmpty()
                .WithMessage("Id of extractor cannot be empty.");

            RuleFor(_ => _.AmountToExtract).GreaterThanOrEqualTo(0)
                .WithMessage("Amount to be extracted cannot be less than 0.");
        }
    }
}
