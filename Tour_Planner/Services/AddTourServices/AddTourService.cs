using System.Threading.Tasks;
using DataAccessLayer;
using Models;

namespace Tour_Planner.Services.AddTourServices;

public class AddTourService : TourServiceBase, IAddTourService
{
    private readonly IUnitofWork _unitofWork;
    
    public AddTourService(IUnitofWork unitofWork)
    {
        _unitofWork = unitofWork;
    }
    
    public async Task AddTour(Tour tour)
    { 
        
        /*_unitofWork.ToursRepository.AddTour(tour);*/
        await _unitofWork.Commit();
    }
}