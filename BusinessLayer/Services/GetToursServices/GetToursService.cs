using DataAccessLayer;
using Models;

namespace BusinessLayer.Services.GetToursService;

public class GetToursService : TourServiceBase, IGetToursService
{
    private readonly IUnitofWorkFactory _unitofWorkFactory;
    
    public GetToursService(IUnitofWorkFactory unitofWorkFactory)
    {
        _unitofWorkFactory = unitofWorkFactory;
    }
    
    public async Task<IEnumerable<Tour>> GetTours()
    {
        var unitofWork = _unitofWorkFactory.CreateUnitofWork();
        var toursDTO = unitofWork.ToursRepository.GetTours();
        await unitofWork.Commit();
        return toursDTO.Select(tourDTO => ConvertToTourModel(tourDTO));
    }
}