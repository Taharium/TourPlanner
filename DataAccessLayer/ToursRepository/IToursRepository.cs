using Models;

namespace DataAccessLayer.ToursRepository;

public interface IToursRepository
{
    IEnumerable<Tour> GetTours();
    void AddTour(Tour tour);
    void DeleteTour(Tour tour);
    void UpdateTour(Tour tour);
    void Save();
    
}