using DataAccessLayer.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class TourPlannerDbContext : DbContext
{
    public TourPlannerDbContext(DbContextOptions<TourPlannerDbContext> options) : base(options) {}
    
    public DbSet<TourDTO> Tours { get; set; }
    public DbSet<TourLogsDTO> TourLogs { get; set; }
    
}
