using FluentValidation;

namespace SatisfactoryPlanner.Modules.Resources.Application.ResourceNodeExtractions.ExtractResourceNode
{
    internal class ExtractResourceNodeCommandValidator : AbstractValidator<ExtractResourceNodeCommand>
    {
        public ExtractResourceNodeCommandValidator()
        {
            RuleFor(_ => _.ResourceNodeId).NotEmpty()
                .WithMessage("Id of resource node cannot be empty.");

            RuleFor(_ => _.ResourceExtractorId).NotEmpty()
                .WithMessage("Id of resource extractor cannot be empty.");

            RuleFor(_ => _.Amount).GreaterThanOrEqualTo(0)
                .WithMessage("Amount to be extracted cannot be less than 0.");
        }
    }
}
