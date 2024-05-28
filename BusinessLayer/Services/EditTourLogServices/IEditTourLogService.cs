using Models;

namespace BusinessLayer.Services.EditTourLogServices;

public interface IEditTourLogService {
    Task EditTourLog(Tour tour, TourLogs tourLog);
}