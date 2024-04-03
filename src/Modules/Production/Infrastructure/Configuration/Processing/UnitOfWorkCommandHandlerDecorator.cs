using MediatR;
using Microsoft.EntityFrameworkCore;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Configuration.Processing;
using SatisfactoryPlanner.Modules.Production.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ICommandHandler<T> _decorated;
        private readonly FactoriesContext _factoriesContext;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkCommandHandlerDecorator(ICommandHandler<T> decorated, IUnitOfWork unitOfWork,
            FactoriesContext meetingContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _factoriesContext = meetingContext;
        }

        public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
        {
            await _decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase)
            {
                var internalCommand =
                    await _factoriesContext.InternalCommands.FirstOrDefaultAsync(
                        x => x.Id == command.Id,
                        cancellationToken);

                if (internalCommand != null)
                    internalCommand.ProcessedDate = DateTime.UtcNow;
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}