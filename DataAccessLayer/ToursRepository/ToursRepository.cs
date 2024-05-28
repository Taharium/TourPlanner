using DataAccessLayer.DBContextFactory;
using DataAccessLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccessLayer.ToursRepository;

public class ToursRepository : IToursRepository
{
    private readonly TourPlannerDbContext _dbContext;

    public ToursRepository(TourPlannerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public TourDTO? GetById(int id) {
        return _dbContext.Tours.Find(id);
    }

    public IEnumerable<TourDTO> GetTours() {
        return _dbContext.Tours.Include(tour => tour.TourLogsList).ToList();
    }

    public void AddTour(TourDTO tour)
    {
        _dbContext.Tours.Add(tour);
        
    }

    public void DeleteTour(TourDTO tour)
    { 
        _dbContext.Tours.Remove(tour);
    }

    public void UpdateTour(TourDTO tour) {
        _dbContext.Entry(tour).State = EntityState.Modified;
    }
    
}