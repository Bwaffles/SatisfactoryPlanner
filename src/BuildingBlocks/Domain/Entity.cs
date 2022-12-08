using System.Collections.Generic;

namespace SatisfactoryPlanner.BuildingBlocks.Domain
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        ///     Domain events that have occurred.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        /// <summary>
        ///     Add domain event.
        /// </summary>
        /// <param name="domainEvent">Domain event.</param>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        ///     Check if any rules have been broken.
        /// </summary>
        /// <param name="rule">The rule to check.</param>
        /// <exception cref="BusinessRuleValidationException">Thrown when the <paramref name="rule" /> has been broken.</exception>
        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
                throw new BusinessRuleValidationException(rule);
        }
    }
}