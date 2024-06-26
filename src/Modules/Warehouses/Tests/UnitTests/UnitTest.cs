using FluentAssertions.Execution;

namespace SatisfactoryPlanner.Modules.Warehouses.UnitTests
{
    [TestFixture]
    public class UnitTest
    {
        protected static void AssertAll(Action assert)
        {
            using (new AssertionScope())
                assert();
        }
    }
}
