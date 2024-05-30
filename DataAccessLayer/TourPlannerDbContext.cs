using DataAccessLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

namespace DataAccessLayer;

public class TourPlannerDbContext : DbContext
{
    public TourPlannerDbContext(DbContextOptions<TourPlannerDbContext> options) : base(options) {}
    
    public DbSet<TourDTO> Tours { get; set; }
    public DbSet<TourLogsDTO> TourLogs { get; set; }
    
}
