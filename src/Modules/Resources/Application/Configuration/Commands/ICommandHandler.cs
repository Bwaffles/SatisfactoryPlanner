using MediatR;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands
{
    /// <remarks>
    ///     Abstraction over top of Mediatr.
    /// </remarks>
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
    {
    }

    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
    }
}
