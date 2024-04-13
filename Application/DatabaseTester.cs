using Npgsql;

namespace Application;

public class DatabaseTester
{
    private readonly IConfiguration _configuration;

    public DatabaseTester(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool TestConnection()
    {
        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            // If the connection is successful, return true or log a success message
            Console.WriteLine("Connection successful!");
            return true;
        }
        catch (Exception ex)
        {
            // If the connection fails, log the exception or return false
            Console.WriteLine($"Connection failed: {ex.Message}");
            return false;
        }
    }
}
