using Models;

namespace BusinessLayer;

public interface IBusinessLogicTourLogs
{
    void AddTourLog(Tour tour, TourLogs tourLog);
    void DeleteTourLog(Tour tour, TourLogs tourLog);
    void UpdateTourLog(Tour tour, TourLogs tourLog);
}