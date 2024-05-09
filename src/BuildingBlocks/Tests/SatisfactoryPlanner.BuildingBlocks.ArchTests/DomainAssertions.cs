using FluentAssertions;
using NetArchTest.Rules;
using SatisfactoryPlanner.BuildingBlocks.Domain;
using System.Reflection;
using static SatisfactoryPlanner.BuildingBlocks.ArchTests.DomainAssertions.ConstructorTypeFailure;

namespace SatisfactoryPlanner.BuildingBlocks.ArchTests
{
    public static class DomainAssertions
    {
        public static void AssertValueObjectsHavePrivateConstructorWithParametersForState(this Assembly domainAssembly)
        {
            var valueObjects = Types.InAssembly(domainAssembly)
                .That()
                .Inherit(typeof(ValueObject))
                .GetTypes();

            var failingTypes = new List<ConstructorTypeFailure>();
            foreach (var valueObjectType in valueObjects)
            {
                var hasExpectedConstructor = false;

                const BindingFlags bindingFlags = BindingFlags.DeclaredOnly |
                                                  BindingFlags.Public |
                                                  BindingFlags.Instance;
                var typeNames = valueObjectType.GetFields(bindingFlags).Select(x => x.Name.ToLower()).ToList();
                var propertyNames = valueObjectType.GetProperties(bindingFlags).Select(x => x.Name.ToLower()).ToList();
                typeNames.AddRange(propertyNames);

                var constructors = valueObjectType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
                var failingConstructors = new List<FailingConstructor>();
                foreach (var constructorInfo in constructors)
                {
                    var constructorParameterNames = constructorInfo.GetParameters().Select(x => x.Name!.ToLower()).ToList();
                    var missingStateNames = typeNames.Except(constructorParameterNames).ToList();
                    if (missingStateNames.Count == 0)
                    {
                        hasExpectedConstructor = true;
                        break;
                    }

                    var missingConsturctorParameters = constructorParameterNames.Except(typeNames).ToList();
                    failingConstructors.Add(new FailingConstructor(constructorInfo, missingStateNames, missingConsturctorParameters));
                }

                if (!hasExpectedConstructor)
                    failingTypes.Add(new ConstructorTypeFailure(valueObjectType, failingConstructors));
            }

            failingTypes.Should().BeEmpty();
        }

        public record ConstructorTypeFailure(Type Type, List<FailingConstructor> FailingConstructors)
        {
            public record FailingConstructor(ConstructorInfo ConstructorInfo, List<string> TypeStateMissingInConstructorParameters, List<string> ConstructorParametersMissingAsTypeState);
        }
    }
}
