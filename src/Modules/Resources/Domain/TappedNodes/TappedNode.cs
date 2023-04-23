using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes.Rules;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes
{
    public class TappedNode : Entity, IAggregateRoot
    {
        private ExtractorId? _extractorId;
        private readonly string _name;
        private readonly NodeId _nodeId;
        private readonly WorldId _worldId;
        private ExtractionRate _extractionRate;

        public TappedNodeId Id { get; }

        private TappedNode(WorldId worldId, NodeId nodeId)
        {
            Id = new TappedNodeId(Guid.NewGuid());
            _worldId = worldId;
            _nodeId = nodeId;
            _extractorId = null;
            _extractionRate = ExtractionRate.Of(0);
            _name = ""; // TODO can probably remove this since I've pre-created the names for the nodes already
        }

        // ReSharper disable once UnusedMember.Local
        private TappedNode()
        { /* for EF */
        }

        public static TappedNode Spawn(WorldId worldId, NodeId nodeId)
            => new(worldId, nodeId);

        public void Tap(ExtractorId extractorId)
        {
            CheckRule(new NodeCannotAlreadyBeTappedRule(_extractorId));
            //CheckRule(new ExtractorMustBeAbleToExtractResourceRule(extractor, _resourceId));

            _extractorId = extractorId;

            AddDomainEvent(new NodeTappedDomainEvent(Id, _worldId, _nodeId, _extractorId));
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