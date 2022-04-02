using SatisfactoryPlanner.Modules.Factories.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Application.Factories.BuildSubFactory
{
    public class BuildSubFactoryCommand : CommandBase<Guid>
    {
        public BuildSubFactoryCommand(Guid builtUnderFactoryId, string name)
        {
            BuiltUnderFactoryId = builtUnderFactoryId;
            Name = name;
        }

        public Guid BuiltUnderFactoryId { get; }

        public string Name { get; }
    }
}
