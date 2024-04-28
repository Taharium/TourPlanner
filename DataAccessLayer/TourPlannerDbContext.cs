using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

namespace DataAccessLayer;

public class TourPlannerDbContext : DbContext
{
    private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();
    
    public DbSet<Tour> Tours { get; set; }
    public DbSet<TourLogs> TourLogs { get; set; }
    
    /*public void EnsureDb()
    {
        try
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }*/

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Tour_PlannerDB"));
    }
}
