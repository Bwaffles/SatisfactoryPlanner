using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails
{
    public class GetWorldNodeDetailsQuery : QueryBase<WorldNodeDetailsDto>
    {
        public Guid WorldId { get; }

        public Guid NodeId { get; }

        public GetWorldNodeDetailsQuery(Guid worldId, Guid nodeId)
        {
            WorldId = worldId;
            NodeId = nodeId;
        }
    }
}