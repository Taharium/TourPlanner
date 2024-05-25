namespace DataAccessLayer.DBContextFactory;

public interface ITourPlannerDbContextFactory
{
    TourPlannerDbContext CreateDbContext();
}