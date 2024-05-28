using DataAccessLayer;
using Models;

namespace BusinessLayer.Services.AddTourServices;

public class AddTourService : TourServiceBase, IAddTourService {
    private readonly IUnitofWorkFactory _unitofWorkFactory;

    public AddTourService(IUnitofWorkFactory unitofWorkFactory) {
        _unitofWorkFactory = unitofWorkFactory;
    }

    public async Task AddTour(Tour tour) {
        var tourDTO = ConvertToTourDTO(tour);
        using var unitofWork = _unitofWorkFactory.CreateUnitofWork();
        unitofWork.ToursRepository.AddTour(tourDTO);
        await unitofWork.Commit();
        tour.Id = tourDTO.Id;
    }
}