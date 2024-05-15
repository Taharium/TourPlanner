using System.Threading.Tasks;
using Models;

namespace Tour_Planner.Services.AddTourServices;

public interface IAddTourService
{
    Task AddTour(Tour tour);
}