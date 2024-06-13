using Microsoft.EntityFrameworkCore;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Configuration.Processing;
using SatisfactoryPlanner.Modules.Warehouses.Application.Configuration;
using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult>(ICommandHandler<T, TResult> decorated,
        IUnitOfWork unitOfWork,
        WarehousesContext context) : ICommandHandler<T, TResult>
        where T : ICommand<TResult>
    {
        private readonly WarehousesContext _context = context;
        private readonly ICommandHandler<T, TResult> _decorated = decorated;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

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