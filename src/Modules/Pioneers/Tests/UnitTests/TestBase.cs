namespace SatisfactoryPlanner.Modules.Pioneers.Domain.UnitTests
{
    public class TestBase
    {
        [TearDown]
        public void AfterEachTest()
        {
            SystemClock.Reset();
        }
    }
}