namespace SatisfactoryPlanner.BuildingBlocks.IntegrationTests.Probing
{
    public class AssertErrorException : Exception
    {
        public AssertErrorException() : base()
        {
        }

        public AssertErrorException(string? message)
            : base(message)
        {
        }

        public AssertErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}