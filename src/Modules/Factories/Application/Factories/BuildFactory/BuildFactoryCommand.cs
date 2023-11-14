using SatisfactoryPlanner.Modules.Factories.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Application.Factories.BuildFactory
{
    public class BuildFactoryCommand : CommandBase<Guid>
    {
        public string Name { get; }

        public BuildFactoryCommand(string name)
        {
            Name = name;
        }
    }
}