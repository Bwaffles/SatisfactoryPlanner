using Microsoft.EntityFrameworkCore;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Configuration.Processing;
using SatisfactoryPlanner.Modules.Production.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : ICommandHandler<T, TResult>
        where T : ICommand<TResult>
    {
        private readonly ICommandHandler<T, TResult> _decorated;
        private readonly ProductionContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkCommandHandlerWithResultDecorator(ICommandHandler<T, TResult> decorated,
            IUnitOfWork unitOfWork,
            ProductionContext context)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
        {
            var result = await _decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase<TResult>)
            {
                var internalCommand = await _context
                    .InternalCommands
                    .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

                if (internalCommand != null)
                    internalCommand.ProcessedDate = DateTime.UtcNow;
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}