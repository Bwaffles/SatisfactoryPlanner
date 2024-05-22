using System.Reflection;

namespace SatisfactoryPlanner.BuildingBlocks.ArchTests
{
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Get a value indicating whether the property can be changed outside of the constructor.
        /// If the property has no setter, or the setter is private or init it is immutable.
        /// </summary>
        public static bool IsMutable(this PropertyInfo property)
        {
            var setMethod = property.SetMethod;
            if (setMethod == null) // when null, property is read-only so obviously immutable
                return false;

            if (setMethod.IsPrivate || setMethod.IsInit())
                return false;

            return true;
        }
    }
}
