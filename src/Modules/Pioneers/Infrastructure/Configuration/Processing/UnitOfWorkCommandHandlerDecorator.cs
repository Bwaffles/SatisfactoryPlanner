using MediatR;
using Microsoft.EntityFrameworkCore;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Configuration.Processing;
using SatisfactoryPlanner.Modules.Worlds.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ICommandHandler<T> _decorated;
        private readonly PioneersContext _pioneersContext;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkCommandHandlerDecorator(ICommandHandler<T> decorated, IUnitOfWork unitOfWork,
            PioneersContext pioneersContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _pioneersContext = pioneersContext;
        }

        public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
        {
            await _decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase)
            {
                var internalCommand =
                    await _pioneersContext.InternalCommands.FirstOrDefaultAsync(
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