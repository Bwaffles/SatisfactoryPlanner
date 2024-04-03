using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Production.Application.Factories.BuildFactory
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