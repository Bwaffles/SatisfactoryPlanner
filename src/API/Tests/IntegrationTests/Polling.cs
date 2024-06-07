using SatisfactoryPlanner.BuildingBlocks.IntegrationTests.Probing;

namespace SatisfactoryPlanner.API.IntegrationTests
{
    public static class Polling
    {
        public static async Task<T> GetEventually<T>(IProbe<T> probe, int timeout)
            where T : class
        {
            var poller = new Poller(timeout);

            return await poller.GetAsync(probe);
        }
    }
}