using DataAccessLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

namespace DataAccessLayer;

public class TourPlannerDbContext : DbContext
{

    /*private readonly IConfigDatabase _configuration;*/
    
    public TourPlannerDbContext(DbContextOptions<TourPlannerDbContext> options) : base(options) {}

    /*public TourPlannerDbContext(IConfigDatabase configuration)
    {
        _configuration = configuration;
    }*/
    
    public DbSet<TourDTO> Tours { get; set; }
    
    public DbSet<TourLogsDTO> TourLogs { get; set; }
    
    
    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseNpgsql(_configuration.ConnectionStringDb);
    }*/
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TourDTO>()
            .HasMany(t => t.TourLogsList)
            .WithOne(tl => tl.Tour)
            .HasForeignKey(tl => tl.TourId);
    }*/
}
