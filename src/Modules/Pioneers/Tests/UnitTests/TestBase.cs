namespace SatisfactoryPlanner.Modules.Worlds.Domain.UnitTests
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