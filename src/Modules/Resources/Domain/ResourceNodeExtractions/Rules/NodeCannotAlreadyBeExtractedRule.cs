using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions.Rules
{
    public class NodeCannotAlreadyBeExtractedRule : IBusinessRule
    {
        private readonly ResourceNodeExtraction _existingResouceNodeExtraction;

        public NodeCannotAlreadyBeExtractedRule(ResourceNodeExtraction existingResouceNodeExtraction)
        {
            _existingResouceNodeExtraction = existingResouceNodeExtraction;
        }

        public string Message => "Node cannot be extracted more than once.";

        public bool IsBroken() => _existingResouceNodeExtraction != null;
    }
}
