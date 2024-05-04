using Models;

namespace BusinessLayer;

public interface IBusinessLogicTours
{
    IEnumerable<Tour> GetTours();
    void AddTour(Tour tour);
    void DeleteTour(Tour tour);
    void UpdateTour(Tour tour);
}