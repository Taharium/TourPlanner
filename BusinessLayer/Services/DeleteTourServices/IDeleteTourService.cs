using Models;

namespace BusinessLayer.Services.DeleteTourServices;

public interface IDeleteTourService {
    Task DeleteTour(Tour tour);
}