using SatisfactoryPlanner.Modules.Factories.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Application.Factories.BuildSubFactory
{
    public class BuildSubFactoryCommand : CommandBase<Guid>
    {
        public Guid BuiltUnderFactoryId { get; }

        public string Name { get; }

        public BuildSubFactoryCommand(Guid builtUnderFactoryId, string name)
        {
            BuiltUnderFactoryId = builtUnderFactoryId;
            Name = name;
        }
    }
}