using SatisfactoryPlanner.Modules.Pioneers.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.SpawnPioneer
{
    public class SpawnPioneerCommandHandler : ICommandHandler<SpawnPioneerCommand, Guid>
    {
        private readonly IPioneersRepository _pioneersRepository;

        public SpawnPioneerCommandHandler(IPioneersRepository pioneersRepository) =>
            _pioneersRepository = pioneersRepository;

        public async Task<Guid> Handle(SpawnPioneerCommand request, CancellationToken cancellationToken)
        {
            var pioneer = Pioneer.Spawn(request.Auth0UserId);
            await _pioneersRepository.AddAsync(pioneer);

            return pioneer.Id.Value;
        }
    }
}