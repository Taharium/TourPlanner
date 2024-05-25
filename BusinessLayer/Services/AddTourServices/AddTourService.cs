using DataAccessLayer;
using Models;

namespace BusinessLayer.Services.AddTourServices;

public class AddTourService : TourServiceBase, IAddTourService
{
    private readonly IUnitofWork _unitofWork;
    
    public AddTourService(IUnitofWork unitofWork)
    {
        _unitofWork = unitofWork;
    }
    
    public async Task AddTour(Tour tour)
    {
        var tourDTO = ConvertToTourDTO(tour);
        
        _unitofWork.ToursRepository.AddTour(tourDTO);
        await _unitofWork.Commit();
    }
}