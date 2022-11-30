using FluentValidation;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Users.CreateCurrentUser
{
    // ReSharper disable once UnusedMember.Global because it's dynamically registered for DI
    internal class CreateCurrentUserCommandValidator : AbstractValidator<CreateCurrentUserCommand>
    {
        public CreateCurrentUserCommandValidator()
        {
            RuleFor(_ => _.Auth0UserId).NotEmpty()
                .WithMessage("Auth0UserId cannot be empty.");
        }
    }
}
