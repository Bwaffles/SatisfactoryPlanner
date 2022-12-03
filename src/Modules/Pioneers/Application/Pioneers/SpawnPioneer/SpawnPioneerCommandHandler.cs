using MediatR;
using SatisfactoryPlanner.Modules.Pioneers.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.SpawnPioneer
{
    // ReSharper disable once UnusedMember.Global
    public class SpawnPioneerCommandHandler : ICommandHandler<SpawnPioneerCommand>
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