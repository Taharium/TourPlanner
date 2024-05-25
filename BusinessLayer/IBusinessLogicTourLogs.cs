using Models;

namespace BusinessLayer;

public interface IBusinessLogicTourLogs
{
    Task AddTourLog(Tour tour, TourLogs tourLog);
    Task DeleteTourLog(Tour tour, TourLogs tourLog);
    Task UpdateTourLog(Tour tour, TourLogs tourLog);
    event Action<TourLogs> AddTourLogEvent;
    event Action<TourLogs> OnTourLogDeleteEvent;
    event Action<TourLogs> OnTourLogUpdateEvent;
}