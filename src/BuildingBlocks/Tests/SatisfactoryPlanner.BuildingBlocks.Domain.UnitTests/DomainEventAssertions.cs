namespace SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests
{
    public static class DomainEventAssertions
    {
        public static void AssertEventIsNotPublished<TEvent>(Entity aggregate, string because = "")
            where TEvent : DomainEventBase
        {
            var eventIsPublished = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<TEvent>().Any();
            if (eventIsPublished)
                throw new Exception($"Expected {typeof(TEvent).Name} event to not be published {because}.");
        }

        public static TEvent AssertPublishedEvent<TEvent>(Entity aggregate) where TEvent : DomainEventBase
        {
            var domainEvent = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<TEvent>().SingleOrDefault();
            if (domainEvent == null)
                throw new Exception($"{typeof(TEvent).Name} event not published.");

            return domainEvent;
        }
    }
}