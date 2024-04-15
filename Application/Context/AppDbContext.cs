using Models;

namespace Application;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<LogType> LogTypes { get; set; }
    
    public AppDbContext()
    {
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            if (_configuration != null)
            {
                // var connectionString = _configuration.GetConnectionString("DefaultConnection");
                var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
            else
            {
                throw new InvalidOperationException("DbContext has not been configured with a connection string.");
            }
        }
    }
}