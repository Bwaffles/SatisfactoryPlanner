using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Configuration.Queries
{
    public abstract class QueryBase<TResult> : IQuery<TResult>
    {
        public Guid Id { get; }

        protected QueryBase() => Id = Guid.NewGuid();

        protected QueryBase(Guid id) => Id = id;
    }
}