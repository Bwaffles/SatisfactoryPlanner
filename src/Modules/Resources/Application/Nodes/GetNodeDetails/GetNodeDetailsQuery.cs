using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodeDetails
{
    public class GetNodeDetailsQuery : QueryBase<NodeDetailsDto>
    {
        public Guid NodeId { get; }

        public Guid WorldId { get; }

        public GetNodeDetailsQuery(Guid nodeId, Guid worldId)
        {
            NodeId = nodeId;
            WorldId = worldId;
        }
    }
}