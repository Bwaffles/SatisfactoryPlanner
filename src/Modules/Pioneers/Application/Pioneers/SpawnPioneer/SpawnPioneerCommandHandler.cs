using SatisfactoryPlanner.Modules.Pioneers.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.SpawnPioneer
{
    // ReSharper disable once UnusedMember.Global
    public class SpawnPioneerCommandHandler : ICommandHandler<SpawnPioneerCommand, Guid>
    {
        private readonly IPioneersCounter _pioneersCounter;
        private readonly IPioneersRepository _pioneersRepository;

        public SpawnPioneerCommandHandler(IPioneersRepository pioneersRepository, IPioneersCounter pioneersCounter)
        {
            _pioneersRepository = pioneersRepository;
            _pioneersCounter = pioneersCounter;
        }

        public async Task<Guid> Handle(SpawnPioneerCommand request, CancellationToken cancellationToken)
        {
            var pioneer = Pioneer.Spawn(request.Auth0UserId, _pioneersCounter);
            await _pioneersRepository.AddAsync(pioneer);

            return pioneer.Id.Value;
        }
    }
}