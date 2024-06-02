using DataAccessLayer;
using Models;

namespace BusinessLayer.Services.GetToursServices;

public class GetToursService : TourServiceBase, IGetToursService
{
    private readonly IUnitofWorkFactory _unitofWorkFactory;
    
    public GetToursService(IUnitofWorkFactory unitofWork)
    {
        _unitofWorkFactory = unitofWork;
    }
    
    public async Task<IEnumerable<Tour>> GetTours()
    {
        using var unitofWork = _unitofWorkFactory.CreateUnitofWork();
        var toursDTO = unitofWork.ToursRepository.GetTours();
        await unitofWork.Commit();

        IList<Tour> toReturn = [];
        
        foreach (var tourDto in toursDTO) {
            Tour tour = ConvertToTourModel(tourDto);
            toReturn.Add(tour);
        }

        return toReturn;
    }
}