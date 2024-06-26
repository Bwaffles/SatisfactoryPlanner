﻿using SatisfactoryPlanner.Modules.Production.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Application.ProductionLines.SetUpProductionLine
{
    internal class SetUpProductionLineCommandHandler(IProductionLineRepository productionLineRepository, IProductionLineCounter productionLineCounter) : ICommandHandler<SetUpProductionLineCommand, Guid>
    {
        public async Task<Guid> Handle(SetUpProductionLineCommand request, CancellationToken cancellationToken)
        {
            var productionLine = ProductionLine.SetUp(new WorldId(request.WorldId), ProductionLineName.As(request.Name), productionLineCounter);

            await productionLineRepository.AddAsync(productionLine);

            return productionLine.Id.Value;
        }
    }
}
