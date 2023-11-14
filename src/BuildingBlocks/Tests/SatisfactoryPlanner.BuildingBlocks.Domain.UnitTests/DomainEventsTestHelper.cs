using System.Collections;
using System.Reflection;

namespace SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests
{
    public class DomainEventsTestHelper
    {
        public static List<IDomainEvent> GetAllDomainEvents(Entity aggregate)
        {
            var domainEvents = new List<IDomainEvent>();

            if (aggregate.DomainEvents != null)
                domainEvents.AddRange(aggregate.DomainEvents);

            var fields = GetFields(aggregate);
            foreach (var field in fields)
            {
                var isEntity = typeof(Entity).IsAssignableFrom(field.FieldType);
                if (isEntity && field.GetValue(aggregate) is Entity entity)
                    domainEvents.AddRange(GetAllDomainEvents(entity).ToList());

                if (field.FieldType == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(field.FieldType))
                    continue;

                if (field.GetValue(aggregate) is not IEnumerable enumerable)
                    continue;

                foreach (var item in enumerable)
                    if (item is Entity entityItem)
                        domainEvents.AddRange(GetAllDomainEvents(entityItem));
            }

            return domainEvents;
        }

        public static void ClearAllDomainEvents(Entity aggregate)
        {
            aggregate.ClearDomainEvents();

            var fields = GetFields(aggregate);
            foreach (var field in fields)
            {
                var isEntity = field.FieldType.IsAssignableFrom(typeof(Entity));
                if (isEntity && field.GetValue(aggregate) is Entity entity)
                    ClearAllDomainEvents(entity);

                if (field.FieldType == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(field.FieldType))
                    continue;

                if (field.GetValue(aggregate) is not IEnumerable enumerable)
                    continue;

                foreach (var item in enumerable)
                    if (item is Entity entityItem)
                        ClearAllDomainEvents(entityItem);
            }
        }

        private static FieldInfo[] GetFields(Entity aggregate)
        {
            var baseTypeFields = aggregate
                .GetType()
                .BaseType?
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                .ToList() ?? new List<FieldInfo>();

            return aggregate
                .GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                .Concat(baseTypeFields)
                .ToArray();
        }
    }
}