using FluentValidation;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.SpawnPioneer
{
    internal class SpawnPioneerCommandValidator : AbstractValidator<SpawnPioneerCommand>
    {
        public SpawnPioneerCommandValidator()
        {
            RuleFor(_ => _.PioneerId).NotEmpty()
                .WithMessage("PioneerId cannot be empty.");
        }
    }
}
