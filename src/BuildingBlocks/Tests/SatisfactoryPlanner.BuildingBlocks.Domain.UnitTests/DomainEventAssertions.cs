namespace SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests
{
    public static class DomainEventAssertions
    {
        public static void AssertEventIsNotPublished<T>(Entity aggregate, string because = "")
        {
            var eventIsPublished = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<T>().Any();
            if (eventIsPublished)
                throw new Exception($"Expected {typeof(T).Name} event to not be published {because}.");
        }

        public static T AssertPublishedEvent<T>(Entity aggregate)
        {
            var domainEvent = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<T>().SingleOrDefault();
            if (domainEvent == null)
                throw new Exception($"{typeof(T).Name} event not published.");

            return domainEvent;
        }
    }
}