using Microsoft.AspNetCore.Mvc;
using Mono.Cecil;
using NetArchTest.Rules;

namespace SatisfactoryPlanner.API.ArchTests
{
    [TestFixture]
    public class ControllerTests : ArchTests
    {
        [Test]
        public void ControllerShouldHaveOnlyOneHandler()
        {
            var result = Types.InAssembly(ApiAssembly)
                .That()
                .AreClasses()
                .And()
                .Inherit(typeof(ControllerBase))
                .Should()
                .MeetCustomRule(new OnlyOneHandleRule())
                .GetResult();

            AssertArchTestResult(result);
        }
    }

    public class OnlyOneHandleRule : ICustomRule
    {
        public bool MeetsRule(TypeDefinition type) => type.Methods.Count(method => !method.IsConstructor && method.IsPublic) == 1;
    }
}