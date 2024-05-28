using DataAccessLayer;
using DataAccessLayer.DTOs;
using Models;

namespace BusinessLayer.Services.DeleteTourLogServices;

public class DeleteTourLogService : TourServiceBase, IDeleteTourLogService {
    
    private readonly IUnitofWorkFactory _unitofWorkFactory;
    
    public DeleteTourLogService(IUnitofWorkFactory unitofWorkFactory) {
        _unitofWorkFactory = unitofWorkFactory;
    }
    
    public async Task DeleteTourLog(Tour tour, TourLogs tourLog) {
        var tourLogsDto = ConvertToTourLogsDTO(tour, tourLog);
        using var unitofWork = _unitofWorkFactory.CreateUnitofWork();
        unitofWork.TourLogsRepository.DeleteTourLog(tourLogsDto);
        await unitofWork.Commit();
    }
}