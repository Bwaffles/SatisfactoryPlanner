using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes
{
    public class WorldNode : Entity, IAggregateRoot
    {
        private ExtractorId? _extractorId;
        private readonly string _name;
        private readonly NodeId _nodeId;
        private readonly WorldId _worldId;
        private ExtractionRate _extractionRate;

        public WorldNodeId Id { get; }

        private WorldNode(WorldId worldId, NodeId nodeId)
        {
            Id = new WorldNodeId(Guid.NewGuid());
            _worldId = worldId;
            _nodeId = nodeId;
            _extractorId = null;
            _extractionRate = ExtractionRate.Of(0);
            _name = ""; // TODO can probably remove this since I've pre-created the names for the nodes already
        }

        // ReSharper disable once UnusedMember.Local
        private WorldNode()
        { /* for EF */
        }

        public static WorldNode Spawn(WorldId worldId, NodeId nodeId)
            => new(worldId, nodeId);

        public void Tap(Extractor extractor, ResourceId resourceId)
        {
            CheckRule(new CannotAlreadyBeTappedRule(_extractorId));
            CheckRule(new ExtractorMustBeAbleToExtractResourceRule(extractor, resourceId));

            _extractorId = extractor.Id;

            AddDomainEvent(new WorldNodeTappedDomainEvent(Id, _worldId, _nodeId, _extractorId));
        }

        public void IncreaseExtractionRate(ExtractionRate newExtractionRate, IExtractionRateCalculator extractionRateCalculator)
        {
            CheckRule(new CannotLowerExtractionRateBelowCurrentExtractionRateRule(newExtractionRate, _extractionRate));
            CheckRule(new CannotIncreaseExtractionRateAboveTheMaxExtractionRateRule(newExtractionRate, _nodeId, _extractorId, extractionRateCalculator));

            if (_extractionRate == newExtractionRate)
                return;

            _extractionRate = newExtractionRate;

            AddDomainEvent(new ExtractionRateIncreasedDomainEvent(Id, _extractionRate));
        }
    }
}