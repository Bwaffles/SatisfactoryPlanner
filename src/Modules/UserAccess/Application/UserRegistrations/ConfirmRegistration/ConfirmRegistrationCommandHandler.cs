using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.UserAccess.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.UserRegistrations.ConfirmRegistration
{
    internal class ConfirmRegistrationCommandHandler : ICommandHandler<ConfirmRegistrationCommand>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;

        public ConfirmRegistrationCommandHandler(IUserRegistrationRepository userRegistrationRepository) =>
            _userRegistrationRepository = userRegistrationRepository;

        public async Task<Unit> Handle(ConfirmRegistrationCommand command, CancellationToken cancellationToken)
        {
            var userRegistration =
                await _userRegistrationRepository.GetByIdAsync(new UserRegistrationId(command.UserRegistrationId));
            if (userRegistration == null)
                throw new InvalidCommandException("User registration not found.");

            userRegistration.Confirm();

            return Unit.Value;
        }
    }
}