namespace _build;

public class DatabaseConfiguration
{
    public static DatabaseConfiguration IntegrationTests = new("1402");
    public static DatabaseConfiguration Development = new("1401");

    public string User { get; }

    public string Password { get; }

    public string Port { get; }

    public string ServerConnectionString { get; }


    public string ConnectionString { get; }

    DatabaseConfiguration(string port)
    {
        User = "build-user";
        Password = "123qwe!@#QWE";
        Port = port;
        ServerConnectionString = $"Server=127.0.0.1:{Port};User Id={User};Password={Password};";
        ConnectionString = $"{ServerConnectionString}Database=satisfactory-planner;Log Parameters=true;Include Error Detail=true;";
    }
}