using Models;

namespace BusinessLayer.Services.DeleteTourLogServices;

public interface IDeleteTourLogService {
    Task DeleteTourLog(Tour tour, TourLogs tourLog);
}