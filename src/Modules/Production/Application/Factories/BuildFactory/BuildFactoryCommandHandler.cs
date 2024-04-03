using SatisfactoryPlanner.Modules.Production.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Production.Domain.Factories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Application.Factories.BuildFactory
{
    internal class BuildFactoryCommandHandler : ICommandHandler<BuildFactoryCommand, Guid>
    {
        private readonly IFactoryRepository _factoriesRepository;

        public BuildFactoryCommandHandler(IFactoryRepository factoriesRepository)
        {
            _factoriesRepository = factoriesRepository;
        }

        public async Task<Guid> Handle(BuildFactoryCommand request, CancellationToken cancellationToken)
        {
            var factory = Factory.Build(request.Name);
            await _factoriesRepository.AddAsync(factory);

            return factory.Id.Value;
        }
    }
}