using System.Reflection;

namespace SatisfactoryPlanner.BuildingBlocks.ArchTests
{
    public static class MethodInfoExtensions
    {
        public static bool IsInit(this MethodInfo methodInfo)
        {
            // Get the modifiers applied to the return parameter.
            var setMethodReturnParameterModifiers = methodInfo.ReturnParameter.GetRequiredCustomModifiers();

            // Init-only properties are marked with the IsExternalInit type.
            return setMethodReturnParameterModifiers.Contains(typeof(System.Runtime.CompilerServices.IsExternalInit));
        }
    }
}
