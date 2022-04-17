using FluentValidation;

namespace SatisfactoryPlanner.Modules.Resources.Application.ResourceNodeExtractions.ExtractResourceNode
{
    internal class ExtractResourceNodeCommandValidator : AbstractValidator<ExtractResourceNodeCommand>
    {
        public ExtractResourceNodeCommandValidator()
        {
            RuleFor(_ => _.NodeId).NotEmpty()
                .WithMessage("Id of node cannot be empty.");

            RuleFor(_ => _.ExtractorId).NotEmpty()
                .WithMessage("Id of extractor cannot be empty.");

            RuleFor(_ => _.Amount).GreaterThanOrEqualTo(0)
                .WithMessage("Amount to be extracted cannot be less than 0.");
        }
    }
}
