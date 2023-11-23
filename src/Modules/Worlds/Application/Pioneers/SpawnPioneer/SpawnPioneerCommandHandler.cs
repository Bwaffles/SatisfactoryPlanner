using MediatR;
using SatisfactoryPlanner.Modules.Worlds.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers;

namespace SatisfactoryPlanner.Modules.Worlds.Application.Pioneers.SpawnPioneer
{
    // ReSharper disable once UnusedMember.Global
    internal class SpawnPioneerCommandHandler : ICommandHandler<SpawnPioneerCommand>
    {
        private readonly IPioneersRepository _pioneersRepository;

        public SpawnPioneerCommandHandler(IPioneersRepository pioneersRepository)
        {
            _pioneersRepository = pioneersRepository;
        }

        public async Task<Unit> Handle(SpawnPioneerCommand request, CancellationToken cancellationToken)
        {
            var pioneer = Pioneer.Spawn(request.PioneerId);

            await _pioneersRepository.AddAsync(pioneer);

            return Unit.Value;
        }
    }
}