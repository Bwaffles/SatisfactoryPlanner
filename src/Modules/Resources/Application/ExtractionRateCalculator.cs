using SatisfactoryPlanner.Modules.Resources.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application
{
    public class ExtractionRateCalculator : IExtractionRateCalculator
    {
        private readonly IExtractorRepository _extractorRepository;
        private readonly INodeRepository _nodeRepository;

        public ExtractionRateCalculator(INodeRepository nodeRepository, IExtractorRepository extractorRepository)
        {
            _nodeRepository = nodeRepository;
            _extractorRepository = extractorRepository;
        }
        
        public ExtractionRate GetMaxExtractionRate(NodeId nodeId, ExtractorId extractorId)
        {
            var node = _nodeRepository.FindById(nodeId);
            if (node == null) throw new InvalidOperationException();

            var extractor = _extractorRepository.FindById(extractorId);
            if (extractor == null) throw new InvalidOperationException();

            var maxExtractionRate = ResourceExtractionCalculator.GetMaxExtractionRate(extractor, node);
            return ExtractionRate.Of(maxExtractionRate);
        }
    }
}