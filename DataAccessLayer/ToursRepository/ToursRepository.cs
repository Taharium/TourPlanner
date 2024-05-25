using DataAccessLayer.DBContextFactory;
using DataAccessLayer.DTOs;
using Models;

namespace DataAccessLayer.ToursRepository;

public class ToursRepository : IToursRepository
{
    private readonly TourPlannerDbContext _dbContext;

    public ToursRepository(ITourPlannerDbContextFactory contextFactory)
    {
        _dbContext = contextFactory.CreateDbContext();
    }
    
    public IEnumerable<TourDTO> GetTours() => _dbContext.Tours.ToList();

    public void AddTour(TourDTO tour)
    {
        _dbContext.Tours.Add(tour);
        
    }

    public void DeleteTour(TourDTO tour)
    {
        _dbContext.Tours.Remove(tour);
    }

    public void UpdateTour(TourDTO tour) {
        var entry = _dbContext.Tours.Find(tour.Id) ?? throw new ArgumentException("Tour not found with this Id");
        
        _dbContext.Entry(entry).CurrentValues.SetValues(tour);
    }
    
}