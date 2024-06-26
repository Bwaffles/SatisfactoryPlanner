﻿using SatisfactoryPlanner.Modules.Production.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Production.Domain.Factories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Application.Factories.BuildSubFactory
{
    internal class BuildSubFactoryCommandHandler : ICommandHandler<BuildSubFactoryCommand, Guid>
    {
        private readonly IFactoryRepository _factoriesRepository;

        public BuildSubFactoryCommandHandler(IFactoryRepository factoriesRepository)
        {
            _factoriesRepository = factoriesRepository;
        }

        public async Task<Guid> Handle(BuildSubFactoryCommand request, CancellationToken cancellationToken)
        {
            var factory = await _factoriesRepository.GetByIdAsync(new FactoryId(request.BuiltUnderFactoryId));

            var subFactory = factory.BuildSubFactory(request.Name);
            await _factoriesRepository.AddAsync(subFactory);

            return subFactory.Id.Value;
        }
    }
}