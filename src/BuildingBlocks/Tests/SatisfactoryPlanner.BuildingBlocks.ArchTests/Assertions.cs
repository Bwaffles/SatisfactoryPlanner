using FluentAssertions;

namespace SatisfactoryPlanner.BuildingBlocks.ArchTests
{
    public static class Assertions
    {
        /// <summary>
        /// Assert that all fields and properties on the types are immutable and can't be changed outside the constructor.
        /// </summary>
        public static void AssertImmutability(this IEnumerable<Type> types)
        {
            var failingTypes = types
                .Select(type => new
                {
                    Type = type,
                    MutableFields = type.GetFields().Where(x => !x.IsInitOnly),
                    MutableProperties = type.GetProperties().Where(property => property.IsMutable())
                })
                .Where(type => type.MutableFields.Any() || type.MutableProperties.Any());

            failingTypes.Should().BeNullOrEmpty();
        }
    }
}
