using DataAccessLayer;
using Models;

namespace BusinessLayer.Services.GetToursService;

public class GetToursService : TourServiceBase, IGetToursService
{
    private readonly IUnitofWork _unitofWork;
    
    public GetToursService(IUnitofWork unitofWork)
    {
        _unitofWork = unitofWork;
    }
    
    public async Task<IEnumerable<Tour>> GetTours()
    {
        var toursDTO = _unitofWork.ToursRepository.GetTours();
        await _unitofWork.Commit();
        return toursDTO.Select(tourDTO => ConvertToTourModel(tourDTO));
    }
}