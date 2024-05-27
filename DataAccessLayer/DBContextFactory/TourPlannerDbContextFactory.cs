using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DBContextFactory;

public class TourPlannerDbContextFactory : ITourPlannerDbContextFactory
{
    private readonly Func<TourPlannerDbContext> _dbcontext;
    
    public TourPlannerDbContextFactory(Func<TourPlannerDbContext> context)
    {
        _dbcontext = context;
    }
    
    public TourPlannerDbContext CreateDbContext()
    {
        return _dbcontext();
    }
}