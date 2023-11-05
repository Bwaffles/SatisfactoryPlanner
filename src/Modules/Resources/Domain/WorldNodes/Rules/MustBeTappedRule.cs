using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules
{
    public class MustBeTappedRule : IBusinessRule
    {
        private readonly bool _isTapped;

        public MustBeTappedRule(bool isTapped)
        {
            _isTapped = isTapped;
        }

        public bool IsBroken() => !_isTapped;

        public string Message => "Node must be tapped.";
    }
}