using Models;

namespace DataAccessLayer.ToursRepository;

public class ToursRepository : IToursRepository
{
    private readonly TourPlannerDbContext _dbContext;

    public ToursRepository(TourPlannerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IEnumerable<Tour> GetTours() => _dbContext.Tours.ToList();

    public void AddTour(Tour tour)
    {
        _dbContext.Tours.Add(tour);
        
    }

    public void DeleteTour(Tour tour)
    {
        _dbContext.Tours.Remove(tour);
    }

    public void UpdateTour(Tour tour)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
       _dbContext.SaveChanges();
    }
}