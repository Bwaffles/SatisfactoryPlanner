using FluentValidation;

namespace SatisfactoryPlanner.Modules.Production.Application.ProductionLines.SetUpProductionLine
{
    internal class SetUpProductionLineCommandValidator : AbstractValidator<SetUpProductionLineCommand>
    {
        public SetUpProductionLineCommandValidator()
        {
            RuleFor(_ => _.WorldId).NotEmpty().WithMessage("WorldId cannot be empty.");
        }
    }
}
