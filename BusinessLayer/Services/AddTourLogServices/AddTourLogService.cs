using DataAccessLayer;
using DataAccessLayer.DTOs;
using Models;

namespace BusinessLayer.Services.AddTourLogServices;

public class AddTourLogService : TourServiceBase, IAddTourLogService {

    private readonly IUnitofWorkFactory _unitofWorkFactory;
    
    public AddTourLogService(IUnitofWorkFactory unitofWorkFactory) {
        _unitofWorkFactory = unitofWorkFactory;
    }
    
    public async Task AddTourLog(Tour tour, TourLogs tourLog) {
        var tourLogsDto = ConvertToTourLogsDTO(tour, tourLog);
        using var unitofWork = _unitofWorkFactory.CreateUnitofWork();
        TourDTO tourDto = unitofWork.ToursRepository.GetById(tour.Id) ?? new();
        tourLogsDto.Tour = tourDto;
        unitofWork.TourLogsRepository.AddTourLog(tourLogsDto);
        await unitofWork.Commit();
        tourLog.Id = tourLogsDto.Id;
    }
}