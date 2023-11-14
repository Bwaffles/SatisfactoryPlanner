using SatisfactoryPlanner.Modules.UserAccess.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Users.CreateCurrentUser
{
    internal class CreateCurrentUserCommandHandler : ICommandHandler<CreateCurrentUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUsersCounter _usersCounter;

        public CreateCurrentUserCommandHandler(IUserRepository userRepository, IUsersCounter usersCounter)
        {
            _userRepository = userRepository;
            _usersCounter = usersCounter;
        }

        public async Task<Guid> Handle(CreateCurrentUserCommand request, CancellationToken cancellationToken)
        {
            var user = User.CreatePioneer(
                request.Auth0UserId,
                _usersCounter
            );

            await _userRepository.AddAsync(user);

            return user.Id.Value;
        }
    }
}