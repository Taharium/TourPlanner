using DataAccessLayer;
using DataAccessLayer.DTOs;
using Models;

namespace BusinessLayer.Services.DeleteTourLogServices;

public class DeleteTourLogService : TourServiceBase, IDeleteTourLogService {
    
    private readonly IUnitofWorkFactory _unitofWorkFactory;
    
    public DeleteTourLogService(IUnitofWorkFactory unitofWorkFactory) {
        _unitofWorkFactory = unitofWorkFactory;
    }
    
    public async Task DeleteTourLog(TourLogs tourLog) {
        var tourLogsDto = ConvertToTourLogsDTO(tourLog);
        using var unitofWork = _unitofWorkFactory.CreateUnitofWork();
        unitofWork.TourLogsRepository.DeleteTourLog(tourLogsDto);
        await unitofWork.Commit();
    }
}