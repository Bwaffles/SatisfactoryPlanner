using FluentValidation;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.SpawnPioneer
{
    internal class SpawnPioneerCommandValidator : AbstractValidator<SpawnPioneerCommand>
    {
        public SpawnPioneerCommandValidator()
        {
            RuleFor(_ => _.Auth0UserId).NotEmpty()
                .WithMessage("Auth0UserId cannot be empty.");
        }
    }
}
