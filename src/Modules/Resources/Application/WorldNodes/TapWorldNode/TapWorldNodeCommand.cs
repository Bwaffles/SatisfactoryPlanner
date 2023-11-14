using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.TapWorldNode
{
    public class TapWorldNodeCommand : CommandBase
    {
        public Guid WorldId { get; }

        public Guid NodeId { get; }

        public Guid ExtractorId { get; }

        public TapWorldNodeCommand(Guid worldId, Guid nodeId, Guid extractorId)
        {
            WorldId = worldId;
            NodeId = nodeId;
            ExtractorId = extractorId;
        }
    }
}