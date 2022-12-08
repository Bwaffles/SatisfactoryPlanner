namespace SatisfactoryPlanner.BuildingBlocks.IntegrationTests
{
    public static class EnvironmentVariablesProvider
    {
        public static string GetVariable(string variableName)
        {
            var environmentVariable = Environment.GetEnvironmentVariable(variableName);

            if (!string.IsNullOrEmpty(environmentVariable))
                return environmentVariable;

            environmentVariable = Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.User);

            if (!string.IsNullOrEmpty(environmentVariable))
                return environmentVariable;

            environmentVariable = Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Machine);
            if (!string.IsNullOrEmpty(environmentVariable))
                return environmentVariable;

            throw new ApplicationException(
                $"Define connection string to integration tests database using environment variable: {variableName}.");
        }
    }
}