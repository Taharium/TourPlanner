using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DBContextFactory;

public class TourPlannerDbContextFactory : ITourPlannerDbContextFactory
{
    private readonly DbContextOptions<TourPlannerDbContext> _options;
    
    public TourPlannerDbContextFactory(DbContextOptions<TourPlannerDbContext> options)
    {
        _options = options;
    }
    
    public TourPlannerDbContext CreateDbContext()
    {
        return new TourPlannerDbContext(_options);
    }
}