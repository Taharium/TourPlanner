using Models;

namespace BusinessLayer.Services.EditTourLogServices;

public interface IEditTourLogService {
    Task EditTourLog(TourLogs tourLog);
}