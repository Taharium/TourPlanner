using Models;
using Models.Enums;

namespace BusinessLayer;

public interface IBusinessLogicTours
{
    IEnumerable<Tour> GetTours();
    Task AddTour(Tour tour);
    Task DeleteTour(Tour tour);
    Task UpdateTour(Tour tour);
    event Action<Tour> AddTourEvent;
    event Action<Tour> OnTourDeleteEvent;
    event Action<Tour> OnTourUpdateEvent;
}