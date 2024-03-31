using Models;

namespace BusinessLayer {
    public interface IBusinessLogic {
        IEnumerable<Tour> GetTours();
        void AddTour(Tour tour);
        void DeleteTour(Tour tour);
        void UpdateTour(Tour tour);
        void AddTourLog(Tour tour, TourLogs tourLog);
        void DeleteTourLog(Tour tour, TourLogs tourLog);
        void UpdateTourLog(Tour tour, TourLogs tourLog);
    }
}
