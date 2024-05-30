namespace SatisfactoryPlanner.BuildingBlocks.IntegrationTests.Probing
{
    public class Poller(int timeoutMillis)
    {
        private readonly int _timeoutMillis = timeoutMillis;
        private readonly int _pollDelayMillis = 1000;

        public async Task CheckAsync(IProbe probe)
        {
            var timeout = new Timeout(_timeoutMillis);
            while (!probe.IsSatisfied())
            {
                if (timeout.HasTimedOut())
                    throw new AssertErrorException(DescribeFailureOf(probe));

                await Task.Delay(_pollDelayMillis);
                await probe.SampleAsync();
            }
        }

        public async Task<T> GetAsync<T>(IProbe<T> probe)
            where T : class
        {
            var timeout = new Timeout(_timeoutMillis);
            var sample = await probe.GetSampleAsync();

            while (!(await probe.IsSatisfiedAsync(sample)))
            {
                if (timeout.HasTimedOut())
                    throw new AssertErrorException(DescribeFailureOf(probe));

                await Task.Delay(_pollDelayMillis);
                sample = await probe.GetSampleAsync();
            }

            return sample;
        }

        private static string DescribeFailureOf(IProbe probe) => probe.DescribeFailureTo();

        private static string DescribeFailureOf<T>(IProbe<T> probe) => DescribeFailureOf(probe);
    }
}