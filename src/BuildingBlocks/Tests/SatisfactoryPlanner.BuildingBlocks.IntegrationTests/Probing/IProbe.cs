namespace SatisfactoryPlanner.BuildingBlocks.IntegrationTests.Probing
{
    public interface IProbe
    {
        bool IsSatisfied();

        Task SampleAsync();

        string DescribeFailureTo();
    }

    public interface IProbe<T>
    {
        Task<bool> IsSatisfiedAsync(T sample);

        Task<T> GetSampleAsync();

        string DescribeFailureTo();
    }
}