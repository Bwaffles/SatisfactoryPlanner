using FluentValidation;

namespace SatisfactoryPlanner.Modules.Production.Application.ProductionLines.SetUpProductionLine
{
    internal class RenameProductionLineCommandValidator : AbstractValidator<RenameProductionLineCommand>
    {
        public RenameProductionLineCommandValidator()
        {
            RuleFor(_ => _.WorldId).NotEmpty().WithMessage("WorldId cannot be empty.");
            RuleFor(_ => _.ProductionLineId).NotEmpty().WithMessage("ProductionLineId cannot be empty.");
        }
    }
}
