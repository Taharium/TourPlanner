using DataAccessLayer;
using DataAccessLayer.DTOs;
using Models;

namespace BusinessLayer.Services.EditTourLogServices;

public class EditTourLogService : TourServiceBase, IEditTourLogService {
    private readonly IUnitofWorkFactory _unitofWorkFactory;
    
    public EditTourLogService(IUnitofWorkFactory unitofWorkFactory) {
        _unitofWorkFactory = unitofWorkFactory;
    }
    
    public async Task EditTourLog(TourLogs tourLog) {
        var tourLogsDto = ConvertToTourLogsDTO(tourLog);
        using var unitofWork = _unitofWorkFactory.CreateUnitofWork();
        unitofWork.TourLogsRepository.UpdateTourLog(tourLogsDto);
        await unitofWork.Commit();
        
    }
}