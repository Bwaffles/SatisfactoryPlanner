using Newtonsoft.Json;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.TappedNodes.SpawnWorldNodes
{
    public class SpawnWorldNodesCommand : InternalCommandBase
    {
        internal Guid WorldId { get; }

        [JsonConstructor]
        public SpawnWorldNodesCommand(Guid id, Guid worldId) : base(id)
            => WorldId = worldId;
    }
}