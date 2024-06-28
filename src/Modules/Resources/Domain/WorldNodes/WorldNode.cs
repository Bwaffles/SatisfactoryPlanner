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
        private readonly NodeId _nodeId;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Used by EF to save world id when spawned.")]
        private readonly WorldId _worldId;
        private ExtractionRate _extractionRate;
        private ExtractorId? _extractorId;

        public WorldNodeId Id { get; }

        private WorldNode(WorldId worldId, NodeId nodeId)
        {
            Id = new WorldNodeId(Guid.NewGuid());
            _worldId = worldId;
            _nodeId = nodeId;
            _extractorId = null;
            _extractionRate = ExtractionRate.Of(0);
        }

        public static WorldNode Spawn(WorldId worldId, NodeId nodeId)
            => new(worldId, nodeId);

        public void Tap(Extractor extractor, ResourceId resourceId)
        {
            CheckRule(new CannotAlreadyBeTappedRule(_extractorId));
            CheckRule(new ExtractorMustBeAbleToExtractResourceRule(extractor, resourceId));

            _extractorId = extractor.Id;

            AddDomainEvent(new WorldNodeTappedDomainEvent(Id, _extractorId));
        }

        public void IncreaseExtractionRate(ExtractionRate extractionRate,
            IExtractionRateCalculator extractionRateCalculator)
        {
            CheckRule(new MustBeTappedRule(IsTapped()));
            CheckRule(new CannotIncreaseExtractionRateBelowCurrentExtractionRateRule(extractionRate, _extractionRate));
            CheckRule(new CannotIncreaseExtractionRateAboveMaxExtractionRateRule(extractionRate, _nodeId, _extractorId!,
                extractionRateCalculator));

            if (_extractionRate == extractionRate)
                return;

            _extractionRate = extractionRate;

            AddDomainEvent(new ExtractionRateIncreasedDomainEvent(Id, _extractionRate.Rate));
        }

        public void DecreaseExtractionRate(ExtractionRate extractionRate)
        {
            CheckRule(new MustBeTappedRule(IsTapped()));
            CheckRule(new CannotDecreaseExtractionRateAboveCurrentExtractionRateRule(extractionRate, _extractionRate));

            if (_extractionRate == extractionRate)
                return;

            _extractionRate = extractionRate;

            AddDomainEvent(new ExtractionRateDecreasedDomainEvent(Id, _extractionRate.Rate));
        }

        public void UpgradeExtractor(Extractor extractor, ResourceId resourceId, Extractor? currentExtractor)
        {
            CheckRule(new MustBeTappedRule(IsTapped()));
            CheckRule(new ExtractorMustBeAbleToExtractResourceRule(extractor, resourceId));
            CheckRule(new CannotUpgradeToASlowerExtractorRule(extractor, currentExtractor!));

            if (_extractorId! == extractor.Id)
                return;

            _extractorId = extractor.Id;

            AddDomainEvent(new ExtractorUpgradedDomainEvent(Id, _extractorId));
        }

        public void DowngradeExtractor(Extractor extractor, ResourceId resourceId, Extractor? currentExtractor,
            IExtractionRateCalculator extractionRateCalculator)
        {
            CheckRule(new MustBeTappedRule(IsTapped()));
            CheckRule(new ExtractorMustBeAbleToExtractResourceRule(extractor, resourceId));
            CheckRule(new CannotDowngradeToAFasterExtractorRule(extractor, currentExtractor!));

            if (_extractorId! == extractor.Id)
                return;

            _extractorId = extractor.Id;

            AddDomainEvent(new ExtractorDowngradedDomainEvent(Id, _extractorId));

            var maxExtractionRate = extractionRateCalculator.GetMaxExtractionRate(_nodeId, _extractorId);
            if (_extractionRate > maxExtractionRate)
            {
                _extractionRate = maxExtractionRate;
                AddDomainEvent(new ExtractionRateDecreasedDomainEvent(Id, _extractionRate.Rate));
            }
        }

        public void DismantleExtractor()
        {
            if (_extractorId is null)
                return;

            _extractorId = null;
            _extractionRate = ExtractionRate.Of(0);

            AddDomainEvent(new ExtractorDismantledDomainEvent(Id));
        }

        private bool IsTapped() => _extractorId is not null;

        public ExtractorId? GetExtractorId() => _extractorId;
    }
}