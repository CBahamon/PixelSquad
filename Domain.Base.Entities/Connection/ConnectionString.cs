namespace Domain.Base.Entities.Connection;

public static class ConnectionString
{
    public static string GetConnectionString()
    {
        var stringConnection = string.Format("Server={0};database={1};User ID={2};Password={3};TrustServerCertificate=True;",
            Environment.GetEnvironmentVariable("Server"),
            Environment.GetEnvironmentVariable("Database"),
            Environment.GetEnvironmentVariable("User"),
            Environment.GetEnvironmentVariable("Password"));
        return stringConnection;
    }

}