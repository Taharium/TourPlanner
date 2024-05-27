using DataAccessLayer;
using Models;

namespace BusinessLayer.Services.EditTourServices;

public class EditTourService : TourServiceBase, IEditTourService {
    private readonly IUnitofWorkFactory _unitofWorkFactory;

    public EditTourService(IUnitofWorkFactory unitofWorkFactory) {
        _unitofWorkFactory = unitofWorkFactory;
    }
    
    
    public async Task EditTour(Tour tour) {
        var tourDTO = ConvertToTourDTO(tour);
        var unitOfWork = _unitofWorkFactory.CreateUnitofWork();
        unitOfWork.ToursRepository.UpdateTour(tourDTO);
        await unitOfWork.Commit();
    }
}