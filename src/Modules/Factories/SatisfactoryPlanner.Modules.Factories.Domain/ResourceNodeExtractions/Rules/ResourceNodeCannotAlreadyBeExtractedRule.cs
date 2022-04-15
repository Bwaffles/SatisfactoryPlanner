using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodeExtractions.Rules
{
    internal class ResourceNodeCannotAlreadyBeExtractedRule : IBusinessRule
    {
        private readonly ResourceNodeExtraction _existingResouceNodeExtraction;

        public ResourceNodeCannotAlreadyBeExtractedRule(ResourceNodeExtraction existingResouceNodeExtraction)
        {
            _existingResouceNodeExtraction = existingResouceNodeExtraction;
        }

        public string Message => "Resource node cannot be extracted more than once.";

        public bool IsBroken() => _existingResouceNodeExtraction != null;
    }
}
