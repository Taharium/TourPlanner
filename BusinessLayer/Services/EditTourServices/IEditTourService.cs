using Models;

namespace BusinessLayer.Services.EditTourServices;

public interface IEditTourService {
    Task EditTour(Tour tour);
}