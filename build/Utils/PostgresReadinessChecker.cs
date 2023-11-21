using Dapper;
using Npgsql;
using Serilog;

namespace Utils;

public static class PostgresReadinessChecker
{
    public static void WaitForPostgresServer(string connectionString)
    {
        using var connection = new NpgsqlConnection(connectionString);
        Thread.Sleep(1000);

        const int maxTryCounts = 30;
        var tryCounts = 0;
        while (true)
        {
            tryCounts++;
            try
            {
                var version = connection.QuerySingle<string>("SELECT version();");
                Log.Information("Postgres Database started. \n" +
                                $"Connection String: {connectionString} \n" +
                                $"Version: {version}");
                break;
            }
            catch (Exception ex)
            {
                Log.Information($"Postgres Database not ready: {ex.Message}");
                if (tryCounts > maxTryCounts)
                    throw new Exception("Postgres Database cannot start.");
            }

            Thread.Sleep(2000);
        }
    }
}