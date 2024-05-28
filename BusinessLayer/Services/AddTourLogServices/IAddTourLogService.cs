using Models;

namespace BusinessLayer.Services.AddTourLogServices;

public interface IAddTourLogService {
    Task AddTourLog(Tour tour, TourLogs tourLog);
}