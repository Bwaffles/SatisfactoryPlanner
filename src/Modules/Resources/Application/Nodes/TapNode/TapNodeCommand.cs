using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes.TapNode
{
    public class TapNodeCommand : CommandBase<Guid>
    {
        public TapNodeCommand(Guid worldId, Guid nodeId, Guid extractorId)
        {
            WorldId = worldId;
            NodeId = nodeId;
            ExtractorId = extractorId;
        }

        public Guid WorldId { get; }

        public Guid NodeId { get; }

        public Guid ExtractorId { get; }
    }
}
