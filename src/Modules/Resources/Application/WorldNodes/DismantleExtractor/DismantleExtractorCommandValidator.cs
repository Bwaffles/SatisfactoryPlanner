using FluentValidation;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DismantleExtractor
{
    // ReSharper disable once UnusedMember.Global
    internal class DismantleExtractorCommandValidator : AbstractValidator<DismantleExtractorCommand>
    {
        public DismantleExtractorCommandValidator()
        {
            RuleFor(_ => _.WorldId).NotEmpty()
                .WithMessage("Id of world cannot be empty.");

            RuleFor(_ => _.NodeId).NotEmpty()
                .WithMessage("Id of node cannot be empty.");
        }
    }
}