﻿using Autofac;
using MediatR;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.Processing
{
    internal static class CommandsExecutor
    {
        internal static async Task Execute(ICommand command)
        {
            using (var scope = WorldsCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(command);
            }
        }

        internal static async Task<TResult> Execute<TResult>(ICommand<TResult> command)
        {
            using (var scope = WorldsCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(command);
            }
        }
    }
}