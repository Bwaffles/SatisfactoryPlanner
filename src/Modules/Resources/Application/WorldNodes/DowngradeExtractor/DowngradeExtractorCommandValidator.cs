using FluentValidation;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DowngradeExtractor
{
    // ReSharper disable once UnusedMember.Global
    internal class DowngradeExtractorCommandValidator : AbstractValidator<DowngradeExtractorCommand>
    {
        public DowngradeExtractorCommandValidator()
        {
            RuleFor(_ => _.WorldId).NotEmpty()
                .WithMessage("Id of world cannot be empty.");

            RuleFor(_ => _.NodeId).NotEmpty()
                .WithMessage("Id of node cannot be empty.");

            RuleFor(_ => _.ExtractorId).NotEmpty()
                .WithMessage("Id of extractor cannot be empty.");
        }
    }
}