using MediatR;
using System;

namespace SatisfactoryPlanner.UserAccess.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
        Guid Id { get; }
    }
}
