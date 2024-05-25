using Models;

namespace BusinessLayer.Services.AddTourServices;

public interface IAddTourService
{
    Task AddTour(Tour tour);
}