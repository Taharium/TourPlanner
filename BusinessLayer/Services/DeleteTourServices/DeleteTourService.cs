using DataAccessLayer;
using Models;

namespace BusinessLayer.Services.DeleteTourServices;

public class DeleteTourService : TourServiceBase, IDeleteTourService {
    private readonly IUnitofWorkFactory _unitofWorkFactory;

    public DeleteTourService(IUnitofWorkFactory unitofWorkFactory) {
        _unitofWorkFactory = unitofWorkFactory;
    }

    public async Task DeleteTour(Tour tour) {
        var tourDTO = ConvertToTourDTO(tour);
        var unitofWork = _unitofWorkFactory.CreateUnitofWork();
        unitofWork.ToursRepository.DeleteTour(tourDTO);
        await unitofWork.Commit();
    }
}