using SatisfactoryPlanner.Modules.Factories.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Application.Factories.BuildFactory
{
    public class BuildFactoryCommand : CommandBase<Guid>
    {
        public BuildFactoryCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
